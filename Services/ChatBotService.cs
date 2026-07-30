using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChatBotApp.Services
{
    public class ChatBotService
    {
        private readonly MLContext _mlContext;
        private readonly List<string> _sentences;
        private readonly float[][] _vectors;
        private readonly ITransformer _tfidfModel;

        public ChatBotService(string dataFilePath)
        {
            if (!File.Exists(dataFilePath))
                throw new FileNotFoundException($"Data file not found: {dataFilePath}");

            _mlContext = new MLContext();

            _sentences = File.ReadAllLines(dataFilePath)
                             .Where(l => !string.IsNullOrWhiteSpace(l))
                             .ToList();

            if (_sentences.Count == 0)
                throw new InvalidOperationException("Data file is empty!");

            // Train TF-IDF model ONCE on the dataset
            var data = _sentences.Select(s => new TextData { Text = s }).ToList();
            var dataView = _mlContext.Data.LoadFromEnumerable(data);

            var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(TextData.Text));
            _tfidfModel = pipeline.Fit(dataView);

            // Precompute dataset vectors
            _vectors = TransformToVectors(_sentences);
        }

        // Transform text into vectors using the trained model
        private float[][] TransformToVectors(List<string> sentences)
        {
            var data = sentences.Select(s => new TextData { Text = s }).ToList();
            var dataView = _mlContext.Data.LoadFromEnumerable(data);
            var transformed = _tfidfModel.Transform(dataView);

            return _mlContext.Data.CreateEnumerable<TransformedData>(transformed, reuseRowObject: false)
                                  .Select(f =>
                                  {
                                      var denseValues = new float[f.Features.Length];
                                      f.Features.CopyTo(denseValues);
                                      return denseValues;
                                  })
                                  .ToArray();
        }

        public string GetResponse(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
                return "Please enter a valid question.";

            // Transform the question using the SAME model
            var inputVector = TransformToVectors(new List<string> { userInput })[0];

            float bestScore = -1;
            string bestSentence = "I don't know the answer to that.";

            for (int i = 0; i < _sentences.Count; i++)
            {
                float score = CosineSimilarity(inputVector, _vectors[i]);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestSentence = _sentences[i];
                }
            }

            return bestSentence;
        }

        private float CosineSimilarity(float[] v1, float[] v2)
        {
            float dot = 0, mag1 = 0, mag2 = 0;
            for (int i = 0; i < v1.Length; i++)
            {
                dot += v1[i] * v2[i];
                mag1 += v1[i] * v1[i];
                mag2 += v2[i] * v2[i];
            }
            return (float)(dot / (Math.Sqrt(mag1) * Math.Sqrt(mag2) + 1e-10));
        }

        private class TextData
        {
            public string Text { get; set; }
        }

        private class TransformedData
        {
            public VBuffer<float> Features { get; set; }
        }
    }
}
