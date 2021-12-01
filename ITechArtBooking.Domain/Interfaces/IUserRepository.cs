﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechArtBooking.Domain.Models;

namespace ITechArtBooking.Domain.Interfaces
{
    public interface IUserRepository
    {
        IQueryable GetAll();
        User Get(Guid id);
        void Create(User user);
        void Update(User user);
        User Delete(Guid id);
    }
}
