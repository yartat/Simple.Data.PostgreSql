﻿using System;

using NUnit.Framework;

namespace Simple.Data.Npgsql.Test
{
  public class TransactionTest
  {
    [SetUp]
    public void SetUp()
    {
      GlobalTest.Database.Seed();
    }


    [Test]
    public void TestCommit()
    {
      var db = Database.Open();

      using (var tx = db.BeginTransaction())
      {
        try
        {
          var order = tx.Orders.Insert(CustomerId: 1, OrderDate: DateTime.Today);
          tx.OrderItems.Insert(OrderId: order.Id, ItemId: 1, Quantity: 3);
          tx.Commit();
        }
        catch
        {
          tx.Rollback();
          throw;
        }
      }
      Assert.AreEqual(2, db.Orders.All().ToList().Count);
      Assert.AreEqual(2, db.OrderItems.All().ToList().Count);
    }

    [Test]
    public void TestRollback()
    {
      var db = Database.Open();

      using (var tx = db.BeginTransaction())
      {
        var order = tx.Orders.Insert(CustomerId: 1, OrderDate: DateTime.Today);
        tx.OrderItems.Insert(OrderId: order.Id, ItemId: 1, Quantity: 3);
        tx.Rollback();
      }
      Assert.AreEqual(1, db.Orders.All().ToList().Count);
      Assert.AreEqual(1, db.OrderItems.All().ToList().Count);
    }

    [Test]
    public void QueryInsideTransaction()
    {
      var db = Database.Open();

      using (var tx = db.BeginTransaction())
      {
        tx.Users.Insert(Name: "Arthur", Age: 42, Password: "Ladida");
        User u2 = tx.Users.FindByName("Arthur");
        Assert.IsNotNull(u2);
        Assert.AreEqual("Arthur", u2.Name);
      }
    }
  }
}