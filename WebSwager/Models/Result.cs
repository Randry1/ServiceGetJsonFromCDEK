﻿namespace WebSwager.Model;


public class RootObject
{
    public Result result { get; set; }
}

public class Result
{
    public string price { get; set; }
    public int deliveryPeriodMin { get; set; }
    public int deliveryPeriodMax { get; set; }
    public string deliveryDateMin { get; set; }
    public string deliveryDateMax { get; set; }
    public int tariffId { get; set; }
    public double priceByCurrency { get; set; }
    public string currency { get; set; }
    public int percentVAT { get; set; }
    public Services[] services { get; set; }
}

public class Services
{
    public int id { get; set; }
    public string title { get; set; }
    public double price { get; set; }
}

