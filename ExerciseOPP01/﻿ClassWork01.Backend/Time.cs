namespace ClassWork01.Backend;

public class Time
{
    // Fields
    private int _hours;
    private int _minute;
    private int _second;
    private int _millisecond;

    // Constructors
    public Time()
    {
        _hours = 0;
        _minute = 0;
        _second = 0;
        _millisecond = 0;
    }

    public Time(int hours)
    {
        Hours = hours;
    }

    public Time(int hours, int minute)
    {
        Hours = hours;
        Minute = minute;
    }

    public Time(int hours, int minute, int second)
    {
        Hours = hours;
        Minute = minute;
        Second = second;
    }

    public Time(int hours, int minute, int second, int millisecond)
    {
        Hours = hours;
        Minute = minute;
        Second = second;
        Millisecond = millisecond;
    }

    // Properties
    public int Hours
    {
        get => _hours;
        set => _hours = ValidateHours(value);
    }

    public int Minute
    {
        get => _minute;
        set => _minute = ValidateMinute(value);
    }

    public int Second
    {
        get => _second;
        set => _second = ValidateSecond(value);
    }

    public int Millisecond
    {
        get => _millisecond;
        set => _millisecond = ValidateMillisecond(value);
    }


    // Methods
    private int ValidateHours(int hours)
    {
        if (hours < 0 || hours > 23)
        {
            throw new Exception($"The hour: {hours}, is not valid");
        }
        return hours;
    }

    private int ValidateMinute(int minute)
    {
        if (minute < 0 || minute > 59)
        {
            throw new Exception("Minute must be between 0 and 59.");
        }
        return minute;
    }

    private int ValidateSecond(int second)
    {
        if (second < 0 || second > 59)
        {
            throw new Exception("Second must be between 0 and 59.");
        }
        return second;
    }

    private int ValidateMillisecond(int millisecond)
    {
        if (millisecond < 0 || millisecond > 999)
        {
            throw new Exception("Millisecond must be between 0 and 999.");
        }
        return millisecond;
    }


    public override string ToString()
    {
        int displayHour = _hours % 12;

        if (displayHour == 0)
            displayHour = 12;

        string period = _hours < 12 ? "AM" : "PM";

        return $"{displayHour:00}:{_minute:00}:{_second:00}.{_millisecond:000} {period}";
    }

    public int ToMilliseconds()
    {
        return (_hours * 60 * 60 * 1000) +
               (_minute * 60 * 1000) +
               (_second * 1000) +
               _millisecond;
    }

    public int ToSeconds()
    {
        return (_hours * 60 * 60) +
               (_minute * 60) +
               _second;
    }

    public int ToMinutes()
    {
        return (_hours * 60) +
               _minute;
    }

    public Time Add(Time other)
    {
        int newMilliseconds = _millisecond + other._millisecond;
        int newSeconds = _second + other._second;
        int newMinutes = _minute + other._minute;
        int newHours = _hours + other._hours;

        // Milliseconds overflow
        if (newMilliseconds > 999)
        {
            newMilliseconds -= 1000;
            newSeconds += 1;
        }

        // Seconds overflow
        if (newSeconds > 59)
        {
            newSeconds -= 60;
            newMinutes += 1;
        }

        // Minutes overflow
        if (newMinutes > 59)
        {
            newMinutes -= 60;
            newHours += 1;
        }

        // Hours overflow (next day)
        if (newHours > 23)
        {
            newHours -= 24;
        }

        return new Time(newHours, newMinutes, newSeconds, newMilliseconds);
    }

    public bool IsOtherDay(Time other)
    {
        int newMilliseconds = _millisecond + other._millisecond;
        int newSeconds = _second + other._second;
        int newMinutes = _minute + other._minute;
        int newHours = _hours + other._hours;

        if (newMilliseconds > 999)
        {
            newSeconds += 1;
        }

        if (newSeconds > 59)
        {
            newMinutes += 1;
        }

        if (newMinutes > 59)
        {
            newHours += 1;
        }

        if (newHours > 23)
        {
            return true;
        }

        return false;
    }

}
