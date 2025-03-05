# BlazorGeminiChat

基于Blazor WebAssembly构建的Gemini AI聊天应用。

## 访问地址

应用托管在GitHub Pages上，可以通过以下链接访问：

- [https://aliceljy.github.io/BlazorGeminiChat/](https://aliceljy.github.io/BlazorGeminiChat/)

## 项目特点

- 使用Blazor WebAssembly构建的单页应用(SPA)
- 连接到Gemini AI API进行对话
- 响应式设计，适配移动端和桌面端
- 支持聊天历史保存和加载

## 本地开发

### 环境要求

- .NET 9.0 SDK
- 现代浏览器支持

### 运行项目

1. 克隆仓库
   ```
   git clone https://github.com/AliceLJY/BlazorGeminiChat.git
   cd BlazorGeminiChat
   ```

2. 还原依赖
   ```
   dotnet restore
   ```

3. 运行项目
   ```
   dotnet run
   ```

4. 在浏览器中访问 `https://localhost:5001` 或 `http://localhost:5000`

## 部署

项目配置为使用GitHub Actions自动部署到GitHub Pages。当推送到main分支时，会自动构建和部署应用。

### 手动部署

如需手动部署，可执行以下命令：

```
dotnet publish -c Release
```

发布的文件位于 `bin/Release/net9.0/publish/wwwroot` 目录下。

## 后端API

应用使用位于 `https://gmbot.run-us-west2.goorm.site` 的后端API服务。如需修改API地址，请编辑 `Program.cs` 文件中的 `BaseAddress` 设置。
