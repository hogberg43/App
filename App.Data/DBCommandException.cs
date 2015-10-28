using System;

namespace App.Data
{
    public class DBCommandException : DBException
    {
        public DBCommandException() { }
        public DBCommandException(string message) : base(message) { }
        public DBCommandException(string message, Exception inner) : base(message, inner) { }
       
    }
}
