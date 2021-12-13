﻿using System;

namespace EF6TempTableKit.Test.CustomConverters
{
    /// <summary>
    /// WrongIntCustomConverter converter doesn't implement ICustomConverter. Exception should be thrown.
    /// </summary>
    public class WrongIntCustomConverter
    {
        public Func<int, int> Converter => (x) => x * 100;
    }
}
