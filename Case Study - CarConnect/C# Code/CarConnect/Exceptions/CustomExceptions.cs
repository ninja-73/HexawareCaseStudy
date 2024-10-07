namespace CarConnect.Exceptions
{
    internal class CustomExceptions
    {
        public class AuthenticationException : Exception
        {
            public AuthenticationException() : base("Authentication failed.")
            {
            }

            public AuthenticationException(string message) : base(message)
            {
            }
        }

        // ReservationException
        public class ReservationException : Exception
        {
            public ReservationException() : base("Reservation failed.")
            {
            }

            public ReservationException(string message) : base(message)
            {
            }
        }

        // VehicleNotFoundException
        public class VehicleNotFoundException : Exception
        {
            public VehicleNotFoundException() : base("Vehicle not found.")
            {
            }

            public VehicleNotFoundException(string message) : base(message)
            {
            }
        }

        // AdminNotFoundException
        public class AdminNotFoundException : Exception
        {
            public AdminNotFoundException() : base("Admin user not found.")
            {
            }

            public AdminNotFoundException(string message) : base(message)
            {
            }
        }

        // InvalidInputException
        public class InvalidInputException : Exception
        {
            public InvalidInputException() : base("Invalid input provided.")
            {
            }

            public InvalidInputException(string message) : base(message)
            {
            }
        }

        // DatabaseConnectionException
        public class DatabaseConnectionException : Exception
        {
            public DatabaseConnectionException() : base("Database connection failed.")
            {
            }

            public DatabaseConnectionException(string message) : base(message)
            {
            }

            public DatabaseConnectionException(string message, Exception innerException) : base(message, innerException)
            {
            }
        }

    }
}
