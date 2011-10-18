﻿using System;
using NUnit.Framework;
using Simple.Data.PostgreSql.Test.Utility;

namespace Simple.Data.PostgreSql.Test
{
  public class OrderDetailTest
  {
    [SetUp]
    public void SetUp()
    {
      DatabaseUtility.SeedDatabase("Test");
    }

    [TearDown]
    public void TearDown()
    {
    }

    [Test]
    public void TestOrderDetail()
    {
      var db = Database.Open();
      var order = db.Orders.FindByOrderDate(new DateTime(2010, 10, 10));
      Assert.IsNotNull(order);

      var orderItem = order.OrderItems.FirstOrDefault();
      var item = orderItem.Item;
      Assert.IsNotNull(item);
      Assert.AreEqual("Widget", item.Name);
    }
  }
}