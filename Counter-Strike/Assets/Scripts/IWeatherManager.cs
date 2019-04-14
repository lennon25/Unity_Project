public enum WeatherManagerStatus{
	Shutdown,
	Initializing,
	started
}

public interface IWeatherManager{
	ManagerStatus status {get; }

	void Startup(NetworkService service);
}