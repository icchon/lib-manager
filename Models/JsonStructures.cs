

using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace LibManager.Models.JsonStructures
{
    public class NDLStructure
    {
        public const string BaseUrl = "https://ndlsearch.ndl.go.jp/thumbnail/";
    }
    public class GoogleStructure
    {
        public const string BaseUrl = "https://www.googleapis.com/books/v1/volumes?q=isbn:";

        public static readonly JsonSerializerOptions Options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
        };
        public class VolumeInfo
        {
            public string? Title { get; set; }
            public List<string>? Authors { get; set; }
            public string? PublishedDate { get; set; }
            public string? Description { get; set; }
            public List<IndustryIdentifier>? IndustryIdentifiers { get; set; }
            public ReadingModes? ReadingModes { get; set; }
            public int PageCount { get; set; }
            public string? PrintType { get; set; }
            public string? MaturityRating { get; set; }
            public bool AllowAnonLogging { get; set; }
            public string? ContentVersion { get; set; }
            public PanelizationSummary? PanelizationSummary { get; set; }
            public ImageLinks? ImageLinks { get; set; }
            public string? Language { get; set; }
            public string? PreviewLink { get; set; }
            public string? InfoLink { get; set; }
            public string? CanonicalVolumeLink { get; set; }
        }

        public class IndustryIdentifier
        {
            public string? Type { get; set; }
            public string? Identifier { get; set; }
        }

        public class ReadingModes
        {
            public bool Text { get; set; }
            public bool Image { get; set; }
        }

        public class PanelizationSummary
        {
            public bool ContainsEpubBubbles { get; set; }
            public bool ContainsImageBubbles { get; set; }
        }

        public class ImageLinks
        {
            public string? SmallThumbnail { get; set; }
            public string? Thumbnail { get; set; }
        }

        public class SaleInfo
        {
            public string? Country { get; set; }
            public string? Saleability { get; set; }
            public bool? IsEbook { get; set; }
        }

        public class Epub
        {
            public bool IsAvailable { get; set; }
        }

        public class Pdf
        {
            public bool IsAvailable { get; set; }
        }

        public class AccessInfo
        {
            public string? Country { get; set; }
            public string? Viewability { get; set; }
            public bool Embeddable { get; set; }
            public bool PublicDomain { get; set; }
            public string? TextToSpeechPermission { get; set; }
            public Epub? Epub { get; set; }
            public Pdf? Pdf { get; set; }
            public string? WebReaderLink { get; set; }
            public string? AccessViewStatus { get; set; }
            public bool QuoteSharingAllowed { get; set; }
        }

        public class SearchInfo
        {
            public string? TextSnippet { get; set; }
        }

        public class Item
        {
            public string? Kind { get; set; }
            public string? Id { get; set; }
            public string? Etag { get; set; }
            public string? SelfLink { get; set; }
            public VolumeInfo? VolumeInfo { get; set; }
            public SaleInfo? SaleInfo { get; set; }
            public AccessInfo? AccessInfo { get; set; }
            public SearchInfo? SearchInfo { get; set; }
        }

        public class BookInfo
        {
            public string? Kind { get; set; }
            public int TotalItems { get; set; }
            public List<Item>? Items { get; set; }
        }
    }


    public class OpenBDStructure
    {
        public const string BaseUrl = "https://api.openbd.jp/v1/get?isbn=";

        public static readonly JsonSerializerOptions Options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            WriteIndented = true,
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
        };
        public class BookInfo
        {
            public Onix? Onix { get; set; }
            public Hanmoto? Hanmoto { get; set; }
            public Summary? Summary { get; set; }
        }

        public class Onix
        {
            public CollateralDetail? CollateralDetail { get; set; }
            public string? RecordReference { get; set; }
            public string? NotificationType { get; set; }
            public ProductIdentifier? ProductIdentifier { get; set; }
            public DescriptiveDetail? DescriptiveDetail { get; set; }
            public PublishingDetail? PublishingDetail { get; set; }
            public ProductSupply? ProductSupply { get; set; }
        }

        public class CollateralDetail
        {
        }

        public class ProductIdentifier
        {
            public string? ProductIDType { get; set; }
            public string? IDValue { get; set; }
        }

        public class DescriptiveDetail
        {
            public TitleDetail? TitleDetail { get; set; }
            public List<Contributor>? Contributor { get; set; }
            public Collection? Collection { get; set; }
        }

        public class TitleDetail
        {
            public string? TitleType { get; set; }
            public TitleElement? TitleElement { get; set; }
        }

        public class TitleElement
        {
            public string? TitleElementLevel { get; set; }
            public TitleText? TitleText { get; set; }
        }

        public class TitleText
        {
            public string? Collationkey { get; set; }
            public string? Content { get; set; }
        }

        public class Contributor
        {
            public string? SequenceNumber { get; set; }
            public List<object>? ContributorRole { get; set; }
            public PersonName? PersonName { get; set; }
        }

        public class PersonName
        {
            public string? Content { get; set; }
            public string? Collationkey { get; set; }
        }

        public class Collection
        {
            public string? CollectionType { get; set; }
            public TitleDetail? TitleDetail { get; set; }
        }

        public class PublishingDetail
        {
            public Imprint? Imprint { get; set; }
            public List<PublishingDate>? PublishingDate { get; set; }
        }

        public class Imprint
        {
            public string? ImprintName { get; set; }
        }

        public class PublishingDate
        {
            public string? PublishingDateRole { get; set; }
            public string? Date { get; set; }
        }

        public class ProductSupply
        {
            public SupplyDetail? SupplyDetail { get; set; }
        }

        public class SupplyDetail
        {
            public string? ProductAvailability { get; set; }
            public List<Price>? Price { get; set; }
        }

        public class Price
        {
            public string? PriceType { get; set; }
            public string? CurrencyCode { get; set; }
            public string? PriceAmount { get; set; }
        }

        public class Hanmoto
        {
            public string? Datecreated { get; set; }
            public string? Dateshuppan { get; set; }
            public string? Datemodified { get; set; }
        }

        public class Summary
        {
            public string? Isbn { get; set; }
            public string? Title { get; set; }
            public string? Volume { get; set; }
            public string? Series { get; set; }
            public string? Publisher { get; set; }
            public string? Pubdate { get; set; }
            public string? Cover { get; set; }
            public string? Author { get; set; }
        }
    }

    
}



