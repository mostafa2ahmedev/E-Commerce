﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Common
{
    public class BaseAuditableEntity<TKey> : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public required string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public required string LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; } 
    }
}
