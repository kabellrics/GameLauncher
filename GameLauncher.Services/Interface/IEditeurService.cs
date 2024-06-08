﻿using GameLauncher.Models;

namespace GameLauncher.Services.Interface;
public interface IEditeurService
{
    IEnumerable<Editeur> GetAll();
    IEnumerable<Editeur> GetAllForItem(Guid id);
}