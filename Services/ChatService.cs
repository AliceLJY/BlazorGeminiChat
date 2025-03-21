using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorGeminiChat.Services
{
    public class ChatService
    {
        private readonly HttpClient _httpClient;
        private string _currentSessionId = string.Empty;

        public ChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChatResponse> SendMessageAsync(string message, string? sessionId = null)
        {
            try
            {
                var request = new ChatRequest
                {
                    Message = message,
                    SessionId = sessionId ?? _currentSessionId
                };

                var response = await _httpClient.PostAsJsonAsync("/api/chat", request);
                response.EnsureSuccessStatusCode();
                
                var result = await response.Content.ReadFromJsonAsync<ChatResponse>();
                if (result != null)
                {
                    _currentSessionId = result.SessionId;
                }
                
                return result ?? new ChatResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
                throw;
            }
        }

        public async Task<List<SessionInfo>> GetSessionsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<SessionInfo>>("/api/sessions");
                return response ?? new List<SessionInfo>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting sessions: {ex.Message}");
                return new List<SessionInfo>();
            }
        }

        public async Task<SessionMessages> LoadSessionAsync(string sessionId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<SessionMessages>($"/api/sessions/{sessionId}");
                if (response != null)
                {
                    _currentSessionId = response.SessionId;
                }
                return response ?? new SessionMessages { SessionId = sessionId };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading session: {ex.Message}");
                throw;
            }
        }

        public string GetCurrentSessionId() => _currentSessionId;
    }

    public class ChatRequest
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("session_id")]
        public string SessionId { get; set; } = string.Empty;
    }

    public class ChatResponse
    {
        [JsonPropertyName("sessionId")]
        public string SessionId { get; set; } = string.Empty;

        [JsonPropertyName("response")]
        public string Response { get; set; } = string.Empty;

        [JsonPropertyName("messages")]
        public List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
    }

    public class SessionMessages
    {
        [JsonPropertyName("sessionId")]
        public string SessionId { get; set; } = string.Empty;

        [JsonPropertyName("messages")]
        public List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
    }

    public class ChatMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; } = string.Empty;
    }

    public class SessionInfo
    {
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; } = string.Empty;

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; } = string.Empty;

        [JsonPropertyName("message_count")]
        public int MessageCount { get; set; }

        // 修改为符合新API的属性或处理可能缺少的值
        public string LastMessage { get; set; } = "";
    }
}