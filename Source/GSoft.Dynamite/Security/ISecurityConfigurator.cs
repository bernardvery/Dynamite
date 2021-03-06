﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSoft.Dynamite.Security
{
    using Microsoft.SharePoint;

    /// <summary>
    /// Simple entry point for security configuration on your site collection
    /// </summary>
    public interface ISecurityConfigurator
    {
        /// <summary>
        /// Apply special permissions throughout site hierarchy
        /// </summary>
        /// <param name="site">Site collection</param>
        void ConfigureSiteSecurity(SPSite site);
    }
}
