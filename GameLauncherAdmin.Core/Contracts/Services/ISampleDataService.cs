﻿using GameLauncherAdmin.Core.Models;

namespace GameLauncherAdmin.Core.Contracts.Services;

// Remove this class once your pages/features are using your data.
public interface ISampleDataService
{
    Task<IEnumerable<SampleOrder>> GetContentGridDataAsync();
}
