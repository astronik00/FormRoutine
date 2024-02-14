using Newtonsoft.Json;

namespace Repository.Options;

public class DbConnectionOptions
{
    /// <summary>
    /// Имя хоста
    /// </summary>
    [JsonProperty("Host", Required = Required.Always)]
    public string Host { get; set; }

    /// <summary>
    /// Порт
    /// </summary>
    [JsonProperty("Port", Required = Required.Always)]
    public string Port { get; set; }

    /// <summary>
    /// Название БД
    /// </summary>
    [JsonProperty("Database", Required = Required.Always)]
    public string Database { get; set; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    [JsonProperty("Username", Required = Required.Always)]
    public string Username { get; set; }

    /// <summary>
    /// Пароль
    /// </summary>
    [JsonProperty("Password", Required = Required.Always)]
    public string Password { get; set; }
}