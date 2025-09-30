using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepository;
using KullaniciYonetimi.Domain.Entities;

namespace KullaniciYonetimi.Application.Interfaces;

public interface IUserRoleRepository : IRepository<AppUserRole>
{

}
