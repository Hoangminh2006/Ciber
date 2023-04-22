﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CiberCommon.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiberCommon.Dto
{
    public class ProductDto:EntityDto<int>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public decimal Quantity { get; set; }
    }
}
