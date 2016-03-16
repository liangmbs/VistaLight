using System;

public enum NotificationType {
	Disaster,
	Warning,
	Recommendataion,
	Information,
	Success
}

public class Notification {

	public NotificationType type;

	public DateTime time;
	public string content;

}