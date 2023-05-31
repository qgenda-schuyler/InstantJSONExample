using Newtonsoft.Json.Linq;
using PSPDFKit.Providers;
using PSPDFKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InstantJSONExample.Helper
{
    /// <summary>
    /// Various helpers to reduce duplication.
    /// </summary>
    public static class DocumentHelper
    {
        /// <summary>
        /// Opens the default `Document` asset from `Assets/default.pdf`.
        /// </summary>
        /// <returns><see cref="Document"/> instance.</returns>
        public static Document GetDefaultDocument()
        {
            return GetDocument(GetAssetPath("default.pdf"));
        }

        /// <summary>
        /// Opens a `Document` from the file path given.
        /// </summary>
        /// <returns><see cref="Document"/> instance.</returns>
        public static Document GetDocument(string filePath)
        {
            return new Document(new FileDataProvider(filePath));
        }

        /// <summary>
        /// Returns the absolute path for the file name given relative to the current project `Assets` directory.
        /// </summary>
        /// <returns><see cref="string"/> full path name to the asset.</returns>
        public static string GetAssetPath(string assetFileName)
        {
            return AppDomain.CurrentDomain.BaseDirectory + "Assets/" + assetFileName;
        }

        /// <summary>
        /// Returns the first page of `default.pdf`.
        /// </summary>
        /// <returns><see cref="Page"/> instance.</returns>
        public static Page GetPage()
        {
            return GetDefaultDocument().GetPage(0);
        }

        /// <summary>
        /// Opens `default.pdf` and adds an annotation to the first page.
        /// </summary>
        /// <returns>A <see cref="Document"/> instance.</returns>
        public static Document OpenDocumentAndAnnotation()
        {
            var document = GetDefaultDocument();
            var annotationJson = new JObject
            {
                {"backgroundColor", "#FF0000"},
                {"bbox", new JArray(10, 10, 400, 400)},
                {"creatorName", "Me"},
                {"createdAt", "2018-08-21T14:35:51Z"},
                {"font", "Helvetica"},
                {"fontColor", "#000000"},
                {"fontSize", 72},
                {"fontStyle", new JArray("bold", "italic")},
                {"horizontalAlign", "right"},
                {"id", "01CNEEM7FQCYTBG209AVEMAPKG"},
                {"isFitting", true},
                {"name", "01CNEEKKS2JVPPAEDPKVBYBF65"},
                {"opacity", 1},
                {"pageIndex", 0},
                {"text", "A new text annotation"},
                {"type", "pspdfkit/text"},
                {"updatedAt", "2018-08-21T14:35:51Z"},
                {"v", 1},
                {"verticalAlign", "bottom"}
            };
            document.GetAnnotationProvider().AddAnnotationJson(annotationJson);

            return document;
        }
    }
}
