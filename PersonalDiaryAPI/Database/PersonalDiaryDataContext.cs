using Microsoft.EntityFrameworkCore;
using PersonalDiaryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalDiaryAPI.Database
{
    public class PersonalDiaryDataContext: DbContext
    {
        public PersonalDiaryDataContext(DbContextOptions options)
      : base(options)
        { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<EventModel> Events { get; set; }
    }
}
