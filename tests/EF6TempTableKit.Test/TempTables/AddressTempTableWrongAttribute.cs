﻿using EF6TempTableKit.Attributes;
using EF6TempTableKit.Test.CustomConverters;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF6TempTableKit.Test.TempTables
{
    [Table("#tempAddressTempTableWrongAttribute")]
    public class AddressTempTableWrongAttribute : ITempTable
    {
        [ClusteredIndex]
        [NonClusteredIndex("first")]
        [NonClusteredIndex("second")]
        [TempFieldTypeAttribute("int")]
        [CustomConverterAttribute(typeof(IntCustomConverter))]
        [StringConverter("{0:D}")]
        public string Name { get; set; }
    }

    [NotMapped]
    public class AddressTempTableWrongAttributeDto : AddressTempTableWrongAttribute
    {
    }

    [Table("#tempAddressTempTableWrongAttribute2")]
    public class AddressTempTableWrongAttribute1 : ITempTable
    {
        [ClusteredIndex]
        [NonClusteredIndex("first")]
        [NonClusteredIndex("second")]
        [TempFieldTypeAttribute("int")]
        [CustomConverterAttribute(typeof(WrongIntCustomConverter))]
        public int Id { get; set; }

        [NonClusteredIndex("third")]
        [NonClusteredIndex("second")]
        [TempFieldTypeAttribute("varchar(200)")]
        public string Name { get; set; }
    }

    [NotMapped]
    public class AddressTempTableWrongAttribute1Dto : AddressTempTableWrongAttribute1
    {
    }


    [Table("#tempAddressTempTableWrongAttribute3")]
    public class AddressTempTableWrongAttribute2 : ITempTable
    {
        [ClusteredIndex]
        [NonClusteredIndex("first")]
        [NonClusteredIndex("second")]
        [TempFieldTypeAttribute("int")]
        [CustomConverterAttribute(typeof(IntCustomConverter))]
        public int Id { get; set; }

        [NonClusteredIndex("third")]
        [NonClusteredIndex("second")]
        [TempFieldTypeAttribute("varchar(200)")]
        public string Name { get; set; }
    }

    [NotMapped]
    public class AddressTempTableWrongAttribute2Dto : AddressTempTableWrongAttribute2
    {
    }
}
