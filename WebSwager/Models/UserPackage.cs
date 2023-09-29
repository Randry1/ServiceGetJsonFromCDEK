using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Newtonsoft.Json;
using System.Text;

namespace WebSwager.Model;

public class UserPackage
{
    public string Id { get; set; }
    
    [Required]
    public string fiasSenderCity { get; set; }

    [Required] 
    public string fiasReceiverCity { get; set; }
    
    [Required] 
    public string weight { get; set; }
    
    [Required] 
    public string length { get; set; }
    
    [Required]
    public string width { get; set; }
    
    [Required]
    public string height { get; set; }

    public string ConvertWeight()
    {
        return (float.Parse(weight, CultureInfo.InvariantCulture.NumberFormat) / 1000).ToString();
    }
    
    public string ConvertLength()
    {
        return (float.Parse(length, CultureInfo.InvariantCulture.NumberFormat) / 10).ToString();
    }
    
    public string ConvertWidth()
    {
        return (float.Parse(weight, CultureInfo.InvariantCulture.NumberFormat) / 10).ToString();
    }
    
    public string ConvertHeight()
    {
        return (float.Parse(height, CultureInfo.InvariantCulture.NumberFormat) / 10).ToString();
    }

    private string GetAnswerToCDEK(PackageCDEK new_package_CDEK)
    {
        string result = "";
        using (var client = new HttpClient())
        {
            var uri = new Uri("http://api.cdek.ru/calculator/calculate_price_by_json.php");
            var new_package_CDEK_json = JsonConvert.SerializeObject(new_package_CDEK);
            var request = new StringContent(new_package_CDEK_json, Encoding.UTF8, "application/json");
            var response_CDEK = client.PostAsync(uri, request);
            if (response_CDEK.Result.IsSuccessStatusCode)
            {
                var response_CDEK_string = response_CDEK.Result.Content.ReadAsStringAsync().Result;
                result = response_CDEK_string;
            }
        }

        return result;
    }

    public static CityCDEK[] InitAllCities()
    {
        using (var client = new HttpClient())
        {
            var endpoint = new Uri("http://integration.cdek.ru/v1/location/cities/json?");
            var result = client.GetAsync(endpoint).Result;
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Нет связи сервером СДЕК.");
            }
            var json = result.Content.ReadAsStringAsync().Result;
            var allCities = JsonConvert.DeserializeObject<CityCDEK[]>(json);

            return allCities;
        }
    }

    private string GetCityCode(string fiasGuid)
    {
        string cityCode = "0";

        if (cities != null)
        {
            foreach (CityCDEK city in cities)
            {
                if (city.fiasGuid == fiasGuid)
                {
                    cityCode = city.cityCode;
                }
            }
        }
        else
        {
            cities = InitAllCities();
        }
        return cityCode;
    }

    private string GetDate()
    {
        return DateTime.Now.ToString("yyyy-MM-dd");
    }

    public PackageCDEK ConvertUserPackageInCdek(UserPackage user_package)
    {
        PackageCDEK package_cdek = new PackageCDEK();
        package_cdek.dateExecute = GetDate();
        package_cdek.senderCityId = GetCityCode(user_package.fiasSenderCity);
        package_cdek.receiverCityId = GetCityCode(user_package.fiasReceiverCity);
        string weightCdek = user_package.ConvertWeight();
        string heigthCdek = user_package.ConvertHeight();
        string widthCdek = user_package.ConvertWidth();
        string lengthCdek = user_package.ConvertLength();
        GoodCDEK goodCdek = new GoodCDEK() {weight = weightCdek, height = heigthCdek, width = widthCdek, length = lengthCdek};
        GoodCDEK[] arrGoodCdeks = new GoodCDEK[1];
        arrGoodCdeks[0] = goodCdek;
        package_cdek.goods = arrGoodCdeks;
        package_cdek.tariffId = "480";
        return package_cdek;
    }

    static private CityCDEK[] cities = InitAllCities();

    public AnswerCDEK GetPriceJson() 
    {
        var _package_Cdek = ConvertUserPackageInCdek(this);
        var answerCdek_sting = GetAnswerToCDEK(_package_Cdek);
        var answer_CDEK = JsonConvert.DeserializeObject<AnswerCDEK>(answerCdek_sting);
        if(answer_CDEK == null)
        {
            AnswerCDEK bad_answer_CDEK = new AnswerCDEK() { result = new Result() { } };
            return bad_answer_CDEK;
        }
        else
        {
            return answer_CDEK;
        }
    }

    public Price GetPrice()
    {
        var answerCdek = GetPriceJson();
        if (answerCdek.result != null)
        {
            var price = new Price() { price = answerCdek.result.price };
            return price;
        }
        else
        {
            return new Price() { price = "Невозможно доставить" };
        }
    }
}