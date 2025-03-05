using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorGeminiChat;
using BlazorGeminiChat.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 配置API基础地址 - 这个URL应该指向您的Python后端托管地址
builder.Services.AddScoped(sp => {
    var client = new HttpClient { 
        BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiBaseUrl") ?? 
                         "https://gmbot.run-us-west2.goorm.site") 
    };
    
    // 添加CORS头
    client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
    client.DefaultRequestHeaders.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
    client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Content-Type");
    
    return client;
});

builder.Services.AddScoped<ChatService>();

await builder.Build().RunAsync();