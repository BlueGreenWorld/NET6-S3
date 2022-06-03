using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwsS3.Models
{
    public class AwsCredentials
    {
        public string AwsKey { get; set; } = "";
        public string AwsSecretKey { get; set; } = "";
    }
}