using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Email.Models;
using Microsoft.EntityFrameworkCore;

namespace Email.Data
{
    public class EmailContext : DbContext
    {
        public EmailContext (DbContextOptions<EmailContext> options): base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}