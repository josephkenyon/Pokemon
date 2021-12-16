using System;
using System.Collections.Generic;
using System.Diagnostics;
using Library.Domain;
using Newtonsoft.Json;

namespace Library.Assets.Json
{
    public class ItemsJson
    {
        public ItemType ItemType { get; set; }
        public int? Heal { get; set; }
    }
}
