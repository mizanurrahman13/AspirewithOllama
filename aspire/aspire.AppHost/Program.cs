var builder = DistributedApplication.CreateBuilder(args);

var ollama = builder.AddOllama("ollama")
    .WithDataVolume()
    .WithGPUSupport()
    .WithOpenWebUI();

var deepseek = ollama.AddModel("deepseek", "deepseek-r1:1.5b");
var llama = ollama.AddHuggingFaceModel("llama", "bartowski/Llama-3.2-1B-Instruct-GGUF:IQ4_XS");

builder.AddProject<Projects.aspire>("aspire")
    .WithReference(deepseek)
    .WaitFor(deepseek)
    .WithReference(llama)
    .WaitFor(llama);

builder.Build().Run();
