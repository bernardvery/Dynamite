using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Publishing;

namespace GSoft.Dynamite.Catalogs
{
    /// <summary>
    /// Helper class for Cross Site Publishing operations
    /// </summary>
    public interface ICatalogHelper
    {
        /// <summary>
        /// Set a SharePoint as a product catalog without navigation term associated
        /// Note: For more information, see PublishingCatalogUtility in Microsoft.SharePoint.Publishing
        /// </summary>
        /// <param name="list">The SharePoint list.</param>
        /// <param name="availableFields">List of internal field names that are available through the catalog.</param>
        /// <returns>
        /// The SharePoint list configured as a catalog.
        /// </returns>
        [Obsolete("Use EnsureCatalog(SPWeb, CatalogInfo) instead. We want to limit noise in the *Helper interfaces.")]
        SPList SetListAsCatalog(SPList list, IEnumerable<string> availableFields);

        /// <summary>
        /// Set a SharePoint as a product catalog without navigation term associated
        /// Note: For more information, see PublishingCatalogUtility in Microsoft.SharePoint.Publishing
        /// </summary>
        /// <param name="list">The SharePoint list.</param>
        /// <param name="availableFields">List of internal field names that are available through the catalog.</param>
        /// <param name="activateAnonymousAccess">if set to <c>true</c> [activate anonymous access].</param>
        /// <returns>
        /// The SharePoint list configured as a catalog.
        /// </returns>
        [Obsolete("Use EnsureCatalog(SPWeb, CatalogInfo) instead. We want to limit noise in the *Helper interfaces.")]
        SPList SetListAsCatalog(SPList list, IEnumerable<string> availableFields, bool activateAnonymousAccess);

        /// <summary>
        /// Set a SharePoint as a product catalog with a taxonomy term for navigation.
        /// </summary>
        /// <param name="list">The SharePoint list.</param>
        /// <param name="availableFields">List of internal field names that are available through the catalog.</param>
        /// <param name="taxonomyFieldMap">The taxonomy field that will be used for navigation.</param>
        /// <returns>The SharePoint list configured as a catalog.</returns>
        [Obsolete("Use EnsureCatalog(SPWeb, CatalogInfo) instead. We want to limit noise in the *Helper interfaces.")]
        SPList SetListAsCatalog(SPList list, IEnumerable<string> availableFields, string taxonomyFieldMap);

        /// <summary>
        /// Set a SharePoint as a product catalog with a taxonomy term for navigation.
        /// </summary>
        /// <param name="list">The SharePoint list.</param>
        /// <param name="availableFields">List of internal field names that are available through the catalog.</param>
        /// <param name="taxonomyFieldMap">The taxonomy field that will be used for navigation.</param>
        /// <param name="activateAnonymousAccess">if set to <c>true</c> [activate anonymous access].</param>
        /// <returns>
        /// The SharePoint list configured as a catalog.
        /// </returns>
        [Obsolete("Use EnsureCatalog(SPWeb, CatalogInfo) instead. We want to limit noise in the *Helper interfaces.")]
        SPList SetListAsCatalog(SPList list, IEnumerable<string> availableFields, string taxonomyFieldMap, bool activateAnonymousAccess);

        /// <summary>
        /// Ensure a catalog
        /// </summary>
        /// <param name="web">The web object</param>
        /// <param name="catalog">The catalog</param>
        /// <returns>The list object</returns>
        SPList EnsureCatalog(SPWeb web, CatalogInfo catalog);

        /// <summary>
        /// Ensure catalogs in the web
        /// </summary>
        /// <param name="web">The web</param>
        /// <param name="catalogs">The catalogs</param>
        /// <returns>The catalogs list</returns>
        IEnumerable<SPList> EnsureCatalog(SPWeb web, ICollection<CatalogInfo> catalogs);

        /// <summary>
        /// Method to get a CatalogConnectionSettings from the site
        /// </summary>
        /// <param name="site">The SPSite to get the connection from</param>
        /// <param name="webAbsoluteUrl">The server relative url where the catalog belong</param>
        /// <param name="catalogWebRelativeUrl">The root url of the catalog.</param>
        /// <returns>A catalogConnectionSettings object</returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "1#", Justification = "Uri overload exists. FxCop doesn't see it.")]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "2#", Justification = "Uri overload exists. FxCop doesn't see it.")]
        CatalogConnectionSettings GetCatalogConnectionSettings(SPSite site, string webAbsoluteUrl, string catalogWebRelativeUrl);
        
        /// <summary>
        /// Method to get a CatalogConnectionSettings from the site
        /// </summary>
        /// <param name="site">The SPSite to get the connection from</param>
        /// <param name="webAbsoluteUrl">The full absolute url of the catalog</param>
        /// <param name="catalogWebRelativeUrl">The root url of the catalog.</param>
        /// <returns>A catalogConnectionSettings object</returns>
        CatalogConnectionSettings GetCatalogConnectionSettings(SPSite site, Uri webAbsoluteUrl, Uri catalogWebRelativeUrl);

        /// <summary>
        /// Delete a catalog connection
        /// </summary>
        /// <param name="site">The target site</param>
        /// <param name="catalogConnectionInfo">The catalog connection information</param>
        void DeleteCatalogConnection(SPSite site, CatalogConnectionInfo catalogConnectionInfo);

        /// <summary>
        /// Creates a new catalog connection
        /// </summary>
        /// <param name="site">The target site</param>
        /// <param name="catalogConnectionInfo">The catalog connection information</param>
        /// <param name="overwrite">True if the connection must be override. False otherwise</param>
        /// TODO: make the overwrite param part of the CatalogConnectionInfo object
        void EnsureCatalogConnection(SPSite site, CatalogConnectionInfo catalogConnectionInfo, bool overwrite);

        /// <summary>
        /// Method to create a catalog connection
        /// </summary>
        /// <param name="site">The site where to create the connection</param>
        /// <param name="catalogConnectionSettings">The catalog connection settings to create</param>
        /// <param name="overwriteIfExist">if true and existing, the connection will be deleted then recreated</param>
        void CreateCatalogConnection(SPSite site, CatalogConnectionSettings catalogConnectionSettings, bool overwriteIfExist);

        /// <summary>
        /// Delete a catalog connection
        /// </summary>
        /// <param name="site">The site where to delete the connection</param>
        /// <param name="catalogConnectionSettings">The catalog connection settings to create</param>
        void DeleteCatalogConnection(SPSite site, CatalogConnectionSettings catalogConnectionSettings);
    }
}