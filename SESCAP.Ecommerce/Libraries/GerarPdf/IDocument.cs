using System;
using QuestPDF.Infrastructure;

namespace SESCAP.Ecommerce;

public interface IDocument
{
    DocumentMetadata GetMetadata();
    DocumentSettings GetSettings();

    void Compose(IDocumentContainer container);
}
