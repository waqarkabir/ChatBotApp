# ASP.NET Core Offline Chatbot with ML.NET

This is a **lightweight offline chatbot** built with **ASP.NET Core Web API** and **ML.NET**.  
It can answer questions from a **small dataset (~2000 words)** without using any external AI APIs.  
The chatbot uses **TF‑IDF vectorization** and **cosine similarity** to find the most relevant answer from your dataset.

---

## 🚀 Features
- **Offline** — No internet or API keys required.
- **Fast** — Works instantly for small datasets.
- **Expandable** — Just add more lines to `data.txt`.
- **Safe** — No external dependencies except `Microsoft.ML`.
- **Cross-platform** — Runs on Windows, Linux, and macOS.

---

## 📂 Project Structure

ChatBotApp/ │ ├── Controllers/ │ └── ChatController.cs │ ├── Services/ │ └── ChatBotService.cs │ ├── Program.cs ├── data.txt └── README.md


📌 Example Questions You Can Ask
Here are some example queries based on the Waqar Kabir dataset:

Question	Expected Answer
Who is Waqar Kabir?	Waqar Kabir is a Senior Full Stack .NET Developer with over eight years of professional software development experience.  
What technologies does Waqar Kabir use?	Waqar Kabir specializes in building enterprise web applications using Microsoft technologies.  
Since when has Waqar Kabir been developing .NET applications?	Waqar Kabir has been professionally developing .NET applications since November 2017.  
What frameworks does Waqar Kabir use for scalable apps?	Waqar Kabir develops scalable web applications using ASP.NET Core MVC, Blazor, and React.  
What databases does Waqar Kabir work with?	Waqar Kabir is experienced with Oracle Database, SQL Server, and MongoDB.  
What UI technologies does Waqar Kabir use?	Waqar Kabir develops responsive user interfaces using HTML5, CSS3, Bootstrap, JavaScript, jQuery, and React.  
Does Waqar Kabir have experience with Blazor?	Waqar Kabir has experience developing Blazor Server and Blazor WebAssembly applications.  
What backend skills does Waqar Kabir have?	Waqar Kabir builds REST APIs and integrates third-party services into web applications.  

🧠 How It Works
Load Dataset — Reads data.txt into memory.  
Vectorize — Uses ML.NET's FeaturizeText to convert text into TF‑IDF vectors.  
Compare — Calculates cosine similarity between the question and each dataset entry.  
Respond — Returns the most relevant sentence.  

👨‍💻 Author  
Developed by Waqar Kabir  
Built with ❤️ using ASP.NET Core and ML.NET.  
LinkedIn: https://pk.linkedin.com/in/waqar-kabir-96b8b984
