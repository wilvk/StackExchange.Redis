﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using Moq;
using StackExchange.Redis.KeyspaceIsolation;
using Xunit;

namespace StackExchange.Redis.Tests
{
    public sealed class DatabaseWrapperTests
    {
        private readonly Mock<IDatabase> mock;
        private readonly DatabaseWrapper wrapper;

        public DatabaseWrapperTests()
        {
            mock = new Mock<IDatabase>();
            wrapper = new DatabaseWrapper(mock.Object, Encoding.UTF8.GetBytes("prefix:"));
        }

        [Fact]
        public void CreateBatch()
        {
            object asyncState = new object();
            IBatch innerBatch = new Mock<IBatch>().Object;
            mock.Setup(_ => _.CreateBatch(asyncState)).Returns(innerBatch);
            IBatch wrappedBatch = wrapper.CreateBatch(asyncState);
            mock.Verify(_ => _.CreateBatch(asyncState));
            Assert.IsType<BatchWrapper>(wrappedBatch);
            Assert.Same(innerBatch, ((BatchWrapper)wrappedBatch).Inner);
        }

        [Fact]
        public void CreateTransaction()
        {
            object asyncState = new object();
            ITransaction innerTransaction = new Mock<ITransaction>().Object;
            mock.Setup(_ => _.CreateTransaction(asyncState)).Returns(innerTransaction);
            ITransaction wrappedTransaction = wrapper.CreateTransaction(asyncState);
            mock.Verify(_ => _.CreateTransaction(asyncState));
            Assert.IsType<TransactionWrapper>(wrappedTransaction);
            Assert.Same(innerTransaction, ((TransactionWrapper)wrappedTransaction).Inner);
        }

        [Fact]
        public void DebugObject()
        {
            wrapper.DebugObject("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.DebugObject("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void Get_Database()
        {
            mock.SetupGet(_ => _.Database).Returns(123);
            Assert.Equal(123, wrapper.Database);
        }

        [Fact]
        public void HashDecrement_1()
        {
            wrapper.HashDecrement("key", "hashField", 123, CommandFlags.HighPriority);
            mock.Verify(_ => _.HashDecrement("prefix:key", "hashField", 123, CommandFlags.HighPriority));
        }

        [Fact]
        public void HashDecrement_2()
        {
            wrapper.HashDecrement("key", "hashField", 1.23, CommandFlags.HighPriority);
            mock.Verify(_ => _.HashDecrement("prefix:key", "hashField", 1.23, CommandFlags.HighPriority));
        }

        [Fact]
        public void HashDelete_1()
        {
            wrapper.HashDelete("key", "hashField", CommandFlags.HighPriority);
            mock.Verify(_ => _.HashDelete("prefix:key", "hashField", CommandFlags.HighPriority));
        }

        [Fact]
        public void HashDelete_2()
        {
            RedisValue[] hashFields = new RedisValue[0];
            wrapper.HashDelete("key", hashFields, CommandFlags.HighPriority);
            mock.Verify(_ => _.HashDelete("prefix:key", hashFields, CommandFlags.HighPriority));
        }

        [Fact]
        public void HashExists()
        {
            wrapper.HashExists("key", "hashField", CommandFlags.HighPriority);
            mock.Verify(_ => _.HashExists("prefix:key", "hashField", CommandFlags.HighPriority));
        }

        [Fact]
        public void HashGet_1()
        {
            wrapper.HashGet("key", "hashField", CommandFlags.HighPriority);
            mock.Verify(_ => _.HashGet("prefix:key", "hashField", CommandFlags.HighPriority));
        }

        [Fact]
        public void HashGet_2()
        {
            RedisValue[] hashFields = new RedisValue[0];
            wrapper.HashGet("key", hashFields, CommandFlags.HighPriority);
            mock.Verify(_ => _.HashGet("prefix:key", hashFields, CommandFlags.HighPriority));
        }

        [Fact]
        public void HashGetAll()
        {
            wrapper.HashGetAll("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.HashGetAll("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void HashIncrement_1()
        {
            wrapper.HashIncrement("key", "hashField", 123, CommandFlags.HighPriority);
            mock.Verify(_ => _.HashIncrement("prefix:key", "hashField", 123, CommandFlags.HighPriority));
        }

        [Fact]
        public void HashIncrement_2()
        {
            wrapper.HashIncrement("key", "hashField", 1.23, CommandFlags.HighPriority);
            mock.Verify(_ => _.HashIncrement("prefix:key", "hashField", 1.23, CommandFlags.HighPriority));
        }

        [Fact]
        public void HashKeys()
        {
            wrapper.HashKeys("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.HashKeys("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void HashLength()
        {
            wrapper.HashLength("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.HashLength("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void HashScan()
        {
            wrapper.HashScan("key", "pattern", 123, flags: CommandFlags.HighPriority);
            mock.Verify(_ => _.HashScan("prefix:key", "pattern", 123, 0, 0, CommandFlags.HighPriority));
        }

        [Fact]
        public void HashSet_1()
        {
            HashEntry[] hashFields = new HashEntry[0];
            wrapper.HashSet("key", hashFields, CommandFlags.HighPriority);
            mock.Verify(_ => _.HashSet("prefix:key", hashFields, CommandFlags.HighPriority));
        }

        [Fact]
        public void HashSet_2()
        {
            wrapper.HashSet("key", "hashField", "value", When.Exists, CommandFlags.HighPriority);
            mock.Verify(_ => _.HashSet("prefix:key", "hashField", "value", When.Exists, CommandFlags.HighPriority));
        }

        [Fact]
        public void HashValues()
        {
            wrapper.HashValues("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.HashValues("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void HyperLogLogAdd_1()
        {
            wrapper.HyperLogLogAdd("key", "value", CommandFlags.HighPriority);
            mock.Verify(_ => _.HyperLogLogAdd("prefix:key", "value", CommandFlags.HighPriority));
        }

        [Fact]
        public void HyperLogLogAdd_2()
        {
            RedisValue[] values = new RedisValue[0];
            wrapper.HyperLogLogAdd("key", values, CommandFlags.HighPriority);
            mock.Verify(_ => _.HyperLogLogAdd("prefix:key", values, CommandFlags.HighPriority));
        }

        [Fact]
        public void HyperLogLogLength()
        {
            wrapper.HyperLogLogLength("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.HyperLogLogLength("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void HyperLogLogMerge_1()
        {
            wrapper.HyperLogLogMerge("destination", "first", "second", CommandFlags.HighPriority);
            mock.Verify(_ => _.HyperLogLogMerge("prefix:destination", "prefix:first", "prefix:second", CommandFlags.HighPriority));
        }

        [Fact]
        public void HyperLogLogMerge_2()
        {
            RedisKey[] keys = new RedisKey[] { "a", "b" };
            Expression<Func<RedisKey[], bool>> valid = _ => _.Length == 2 && _[0] == "prefix:a" && _[1] == "prefix:b";
            wrapper.HyperLogLogMerge("destination", keys, CommandFlags.HighPriority);
            mock.Verify(_ => _.HyperLogLogMerge("prefix:destination", It.Is(valid), CommandFlags.HighPriority));
        }

        [Fact]
        public void IdentifyEndpoint()
        {
            wrapper.IdentifyEndpoint("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.IdentifyEndpoint("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void KeyDelete_1()
        {
            wrapper.KeyDelete("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.KeyDelete("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void KeyDelete_2()
        {
            RedisKey[] keys = new RedisKey[] { "a", "b" };
            Expression<Func<RedisKey[], bool>> valid = _ => _.Length == 2 && _[0] == "prefix:a" && _[1] == "prefix:b";
            wrapper.KeyDelete(keys, CommandFlags.HighPriority);
            mock.Verify(_ => _.KeyDelete(It.Is(valid), CommandFlags.HighPriority));
        }

        [Fact]
        public void KeyDump()
        {
            wrapper.KeyDump("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.KeyDump("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void KeyExists()
        {
            wrapper.KeyExists("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.KeyExists("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void KeyExpire_1()
        {
            TimeSpan expiry = TimeSpan.FromSeconds(123);
            wrapper.KeyExpire("key", expiry, CommandFlags.HighPriority);
            mock.Verify(_ => _.KeyExpire("prefix:key", expiry, CommandFlags.HighPriority));
        }

        [Fact]
        public void KeyExpire_2()
        {
            DateTime expiry = DateTime.Now;
            wrapper.KeyExpire("key", expiry, CommandFlags.HighPriority);
            mock.Verify(_ => _.KeyExpire("prefix:key", expiry, CommandFlags.HighPriority));
        }

        [Fact]
        public void KeyMigrate()
        {
            EndPoint toServer = new IPEndPoint(IPAddress.Loopback, 123);
            wrapper.KeyMigrate("key", toServer, 123, 456, MigrateOptions.Copy, CommandFlags.HighPriority);
            mock.Verify(_ => _.KeyMigrate("prefix:key", toServer, 123, 456, MigrateOptions.Copy, CommandFlags.HighPriority));
        }

        [Fact]
        public void KeyMove()
        {
            wrapper.KeyMove("key", 123, CommandFlags.HighPriority);
            mock.Verify(_ => _.KeyMove("prefix:key", 123, CommandFlags.HighPriority));
        }

        [Fact]
        public void KeyPersist()
        {
            wrapper.KeyPersist("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.KeyPersist("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void KeyRandom()
        {
            Assert.Throws<NotSupportedException>(() => wrapper.KeyRandom());
        }

        [Fact]
        public void KeyRename()
        {
            wrapper.KeyRename("key", "newKey", When.Exists, CommandFlags.HighPriority);
            mock.Verify(_ => _.KeyRename("prefix:key", "prefix:newKey", When.Exists, CommandFlags.HighPriority));
        }

        [Fact]
        public void KeyRestore()
        {
            Byte[] value = new Byte[0];
            TimeSpan expiry = TimeSpan.FromSeconds(123);
            wrapper.KeyRestore("key", value, expiry, CommandFlags.HighPriority);
            mock.Verify(_ => _.KeyRestore("prefix:key", value, expiry, CommandFlags.HighPriority));
        }

        [Fact]
        public void KeyTimeToLive()
        {
            wrapper.KeyTimeToLive("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.KeyTimeToLive("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void KeyType()
        {
            wrapper.KeyType("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.KeyType("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void ListGetByIndex()
        {
            wrapper.ListGetByIndex("key", 123, CommandFlags.HighPriority);
            mock.Verify(_ => _.ListGetByIndex("prefix:key", 123, CommandFlags.HighPriority));
        }

        [Fact]
        public void ListInsertAfter()
        {
            wrapper.ListInsertAfter("key", "pivot", "value", CommandFlags.HighPriority);
            mock.Verify(_ => _.ListInsertAfter("prefix:key", "pivot", "value", CommandFlags.HighPriority));
        }

        [Fact]
        public void ListInsertBefore()
        {
            wrapper.ListInsertBefore("key", "pivot", "value", CommandFlags.HighPriority);
            mock.Verify(_ => _.ListInsertBefore("prefix:key", "pivot", "value", CommandFlags.HighPriority));
        }

        [Fact]
        public void ListLeftPop()
        {
            wrapper.ListLeftPop("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.ListLeftPop("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void ListLeftPush_1()
        {
            wrapper.ListLeftPush("key", "value", When.Exists, CommandFlags.HighPriority);
            mock.Verify(_ => _.ListLeftPush("prefix:key", "value", When.Exists, CommandFlags.HighPriority));
        }

        [Fact]
        public void ListLeftPush_2()
        {
            RedisValue[] values = new RedisValue[0];
            wrapper.ListLeftPush("key", values, CommandFlags.HighPriority);
            mock.Verify(_ => _.ListLeftPush("prefix:key", values, CommandFlags.HighPriority));
        }

        [Fact]
        public void ListLength()
        {
            wrapper.ListLength("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.ListLength("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void ListRange()
        {
            wrapper.ListRange("key", 123, 456, CommandFlags.HighPriority);
            mock.Verify(_ => _.ListRange("prefix:key", 123, 456, CommandFlags.HighPriority));
        }

        [Fact]
        public void ListRemove()
        {
            wrapper.ListRemove("key", "value", 123, CommandFlags.HighPriority);
            mock.Verify(_ => _.ListRemove("prefix:key", "value", 123, CommandFlags.HighPriority));
        }

        [Fact]
        public void ListRightPop()
        {
            wrapper.ListRightPop("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.ListRightPop("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void ListRightPopLeftPush()
        {
            wrapper.ListRightPopLeftPush("source", "destination", CommandFlags.HighPriority);
            mock.Verify(_ => _.ListRightPopLeftPush("prefix:source", "prefix:destination", CommandFlags.HighPriority));
        }

        [Fact]
        public void ListRightPush_1()
        {
            wrapper.ListRightPush("key", "value", When.Exists, CommandFlags.HighPriority);
            mock.Verify(_ => _.ListRightPush("prefix:key", "value", When.Exists, CommandFlags.HighPriority));
        }

        [Fact]
        public void ListRightPush_2()
        {
            RedisValue[] values = new RedisValue[0];
            wrapper.ListRightPush("key", values, CommandFlags.HighPriority);
            mock.Verify(_ => _.ListRightPush("prefix:key", values, CommandFlags.HighPriority));
        }

        [Fact]
        public void ListSetByIndex()
        {
            wrapper.ListSetByIndex("key", 123, "value", CommandFlags.HighPriority);
            mock.Verify(_ => _.ListSetByIndex("prefix:key", 123, "value", CommandFlags.HighPriority));
        }

        [Fact]
        public void ListTrim()
        {
            wrapper.ListTrim("key", 123, 456, CommandFlags.HighPriority);
            mock.Verify(_ => _.ListTrim("prefix:key", 123, 456, CommandFlags.HighPriority));
        }

        [Fact]
        public void LockExtend()
        {
            TimeSpan expiry = TimeSpan.FromSeconds(123);
            wrapper.LockExtend("key", "value", expiry, CommandFlags.HighPriority);
            mock.Verify(_ => _.LockExtend("prefix:key", "value", expiry, CommandFlags.HighPriority));
        }

        [Fact]
        public void LockQuery()
        {
            wrapper.LockQuery("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.LockQuery("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void LockRelease()
        {
            wrapper.LockRelease("key", "value", CommandFlags.HighPriority);
            mock.Verify(_ => _.LockRelease("prefix:key", "value", CommandFlags.HighPriority));
        }

        [Fact]
        public void LockTake()
        {
            TimeSpan expiry = TimeSpan.FromSeconds(123);
            wrapper.LockTake("key", "value", expiry, CommandFlags.HighPriority);
            mock.Verify(_ => _.LockTake("prefix:key", "value", expiry, CommandFlags.HighPriority));
        }

        [Fact]
        public void Publish()
        {
            wrapper.Publish("channel", "message", CommandFlags.HighPriority);
            mock.Verify(_ => _.Publish("prefix:channel", "message", CommandFlags.HighPriority));
        }

        [Fact]
        public void ScriptEvaluate_1()
        {
            byte[] hash = new byte[0];
            RedisValue[] values = new RedisValue[0];
            RedisKey[] keys = new RedisKey[] { "a", "b" };
            Expression<Func<RedisKey[], bool>> valid = _ => _.Length == 2 && _[0] == "prefix:a" && _[1] == "prefix:b";
            wrapper.ScriptEvaluate(hash, keys, values, CommandFlags.HighPriority);
            mock.Verify(_ => _.ScriptEvaluate(hash, It.Is(valid), values, CommandFlags.HighPriority));
        }

        [Fact]
        public void ScriptEvaluate_2()
        {
            RedisValue[] values = new RedisValue[0];
            RedisKey[] keys = new RedisKey[] { "a", "b" };
            Expression<Func<RedisKey[], bool>> valid = _ => _.Length == 2 && _[0] == "prefix:a" && _[1] == "prefix:b";
            wrapper.ScriptEvaluate("script", keys, values, CommandFlags.HighPriority);
            mock.Verify(_ => _.ScriptEvaluate("script", It.Is(valid), values, CommandFlags.HighPriority));
        }

        [Fact]
        public void SetAdd_1()
        {
            wrapper.SetAdd("key", "value", CommandFlags.HighPriority);
            mock.Verify(_ => _.SetAdd("prefix:key", "value", CommandFlags.HighPriority));
        }

        [Fact]
        public void SetAdd_2()
        {
            RedisValue[] values = new RedisValue[0];
            wrapper.SetAdd("key", values, CommandFlags.HighPriority);
            mock.Verify(_ => _.SetAdd("prefix:key", values, CommandFlags.HighPriority));
        }

        [Fact]
        public void SetCombine_1()
        {
            wrapper.SetCombine(SetOperation.Intersect, "first", "second", CommandFlags.HighPriority);
            mock.Verify(_ => _.SetCombine(SetOperation.Intersect, "prefix:first", "prefix:second", CommandFlags.HighPriority));
        }

        [Fact]
        public void SetCombine_2()
        {
            RedisKey[] keys = new RedisKey[] { "a", "b" };
            Expression<Func<RedisKey[], bool>> valid = _ => _.Length == 2 && _[0] == "prefix:a" && _[1] == "prefix:b";
            wrapper.SetCombine(SetOperation.Intersect, keys, CommandFlags.HighPriority);
            mock.Verify(_ => _.SetCombine(SetOperation.Intersect, It.Is(valid), CommandFlags.HighPriority));
        }

        [Fact]
        public void SetCombineAndStore_1()
        {
            wrapper.SetCombineAndStore(SetOperation.Intersect, "destination", "first", "second", CommandFlags.HighPriority);
            mock.Verify(_ => _.SetCombineAndStore(SetOperation.Intersect, "prefix:destination", "prefix:first", "prefix:second", CommandFlags.HighPriority));
        }

        [Fact]
        public void SetCombineAndStore_2()
        {
            RedisKey[] keys = new RedisKey[] { "a", "b" };
            Expression<Func<RedisKey[], bool>> valid = _ => _.Length == 2 && _[0] == "prefix:a" && _[1] == "prefix:b";
            wrapper.SetCombineAndStore(SetOperation.Intersect, "destination", keys, CommandFlags.HighPriority);
            mock.Verify(_ => _.SetCombineAndStore(SetOperation.Intersect, "prefix:destination", It.Is(valid), CommandFlags.HighPriority));
        }

        [Fact]
        public void SetContains()
        {
            wrapper.SetContains("key", "value", CommandFlags.HighPriority);
            mock.Verify(_ => _.SetContains("prefix:key", "value", CommandFlags.HighPriority));
        }

        [Fact]
        public void SetLength()
        {
            wrapper.SetLength("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.SetLength("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void SetMembers()
        {
            wrapper.SetMembers("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.SetMembers("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void SetMove()
        {
            wrapper.SetMove("source", "destination", "value", CommandFlags.HighPriority);
            mock.Verify(_ => _.SetMove("prefix:source", "prefix:destination", "value", CommandFlags.HighPriority));
        }

        [Fact]
        public void SetPop()
        {
            wrapper.SetPop("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.SetPop("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void SetRandomMember()
        {
            wrapper.SetRandomMember("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.SetRandomMember("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void SetRandomMembers()
        {
            wrapper.SetRandomMembers("key", 123, CommandFlags.HighPriority);
            mock.Verify(_ => _.SetRandomMembers("prefix:key", 123, CommandFlags.HighPriority));
        }

        [Fact]
        public void SetRemove_1()
        {
            wrapper.SetRemove("key", "value", CommandFlags.HighPriority);
            mock.Verify(_ => _.SetRemove("prefix:key", "value", CommandFlags.HighPriority));
        }

        [Fact]
        public void SetRemove_2()
        {
            RedisValue[] values = new RedisValue[0];
            wrapper.SetRemove("key", values, CommandFlags.HighPriority);
            mock.Verify(_ => _.SetRemove("prefix:key", values, CommandFlags.HighPriority));
        }

        [Fact]
        public void SetScan()
        {
            wrapper.SetScan("key", "pattern", 123, flags: CommandFlags.HighPriority);
            mock.Verify(_ => _.SetScan("prefix:key", "pattern", 123, 0, 0, CommandFlags.HighPriority));
        }

        [Fact]
        public void Sort()
        {
            RedisValue[] get = new RedisValue[] { "a", "#" };
            Expression<Func<RedisValue[], bool>> valid = _ => _.Length == 2 && _[0] == "prefix:a" && _[1] == "#";

            wrapper.Sort("key", 123, 456, Order.Descending, SortType.Alphabetic, "nosort", get, CommandFlags.HighPriority);
            wrapper.Sort("key", 123, 456, Order.Descending, SortType.Alphabetic, "by", get, CommandFlags.HighPriority);

            mock.Verify(_ => _.Sort("prefix:key", 123, 456, Order.Descending, SortType.Alphabetic, "nosort", It.Is(valid), CommandFlags.HighPriority));
            mock.Verify(_ => _.Sort("prefix:key", 123, 456, Order.Descending, SortType.Alphabetic, "prefix:by", It.Is(valid), CommandFlags.HighPriority));
        }

        [Fact]
        public void SortAndStore()
        {
            RedisValue[] get = new RedisValue[] { "a", "#" };
            Expression<Func<RedisValue[], bool>> valid = _ => _.Length == 2 && _[0] == "prefix:a" && _[1] == "#";

            wrapper.SortAndStore("destination", "key", 123, 456, Order.Descending, SortType.Alphabetic, "nosort", get, CommandFlags.HighPriority);
            wrapper.SortAndStore("destination", "key", 123, 456, Order.Descending, SortType.Alphabetic, "by", get, CommandFlags.HighPriority);

            mock.Verify(_ => _.SortAndStore("prefix:destination", "prefix:key", 123, 456, Order.Descending, SortType.Alphabetic, "nosort", It.Is(valid), CommandFlags.HighPriority));
            mock.Verify(_ => _.SortAndStore("prefix:destination", "prefix:key", 123, 456, Order.Descending, SortType.Alphabetic, "prefix:by", It.Is(valid), CommandFlags.HighPriority));
        }

        [Fact]
        public void SortedSetAdd_1()
        {
            wrapper.SortedSetAdd("key", "member", 1.23, When.Exists, CommandFlags.HighPriority);
            mock.Verify(_ => _.SortedSetAdd("prefix:key", "member", 1.23, When.Exists, CommandFlags.HighPriority));
        }

        [Fact]
        public void SortedSetAdd_2()
        {
            SortedSetEntry[] values = new SortedSetEntry[0];
            wrapper.SortedSetAdd("key", values, When.Exists, CommandFlags.HighPriority);
            mock.Verify(_ => _.SortedSetAdd("prefix:key", values, When.Exists, CommandFlags.HighPriority));
        }

        [Fact]
        public void SortedSetCombineAndStore_1()
        {
            wrapper.SortedSetCombineAndStore(SetOperation.Intersect, "destination", "first", "second", Aggregate.Max, CommandFlags.HighPriority);
            mock.Verify(_ => _.SortedSetCombineAndStore(SetOperation.Intersect, "prefix:destination", "prefix:first", "prefix:second", Aggregate.Max, CommandFlags.HighPriority));
        }

        [Fact]
        public void SortedSetCombineAndStore_2()
        {
            RedisKey[] keys = new RedisKey[] { "a", "b" };
            Expression<Func<RedisKey[], bool>> valid = _ => _.Length == 2 && _[0] == "prefix:a" && _[1] == "prefix:b";
            wrapper.SetCombineAndStore(SetOperation.Intersect, "destination", keys, CommandFlags.HighPriority);
            mock.Verify(_ => _.SetCombineAndStore(SetOperation.Intersect, "prefix:destination", It.Is(valid), CommandFlags.HighPriority));
        }

        [Fact]
        public void SortedSetDecrement()
        {
            wrapper.SortedSetDecrement("key", "member", 1.23, CommandFlags.HighPriority);
            mock.Verify(_ => _.SortedSetDecrement("prefix:key", "member", 1.23, CommandFlags.HighPriority));
        }

        [Fact]
        public void SortedSetIncrement()
        {
            wrapper.SortedSetIncrement("key", "member", 1.23, CommandFlags.HighPriority);
            mock.Verify(_ => _.SortedSetIncrement("prefix:key", "member", 1.23, CommandFlags.HighPriority));
        }

        [Fact]
        public void SortedSetLength()
        {
            wrapper.SortedSetLength("key", 1.23, 1.23, Exclude.Start, CommandFlags.HighPriority);
            mock.Verify(_ => _.SortedSetLength("prefix:key", 1.23, 1.23, Exclude.Start, CommandFlags.HighPriority));
        }

        [Fact]
        public void SortedSetLengthByValue()
        {
            wrapper.SortedSetLengthByValue("key", "min", "max", Exclude.Start, CommandFlags.HighPriority);
            mock.Verify(_ => _.SortedSetLengthByValue("prefix:key", "min", "max", Exclude.Start, CommandFlags.HighPriority));
        }

        [Fact]
        public void SortedSetRangeByRank()
        {
            wrapper.SortedSetRangeByRank("key", 123, 456, Order.Descending, CommandFlags.HighPriority);
            mock.Verify(_ => _.SortedSetRangeByRank("prefix:key", 123, 456, Order.Descending, CommandFlags.HighPriority));
        }

        [Fact]
        public void SortedSetRangeByRankWithScores()
        {
            wrapper.SortedSetRangeByRankWithScores("key", 123, 456, Order.Descending, CommandFlags.HighPriority);
            mock.Verify(_ => _.SortedSetRangeByRankWithScores("prefix:key", 123, 456, Order.Descending, CommandFlags.HighPriority));
        }

        [Fact]
        public void SortedSetRangeByScore()
        {
            wrapper.SortedSetRangeByScore("key", 1.23, 1.23, Exclude.Start, Order.Descending, 123, 456, CommandFlags.HighPriority);
            mock.Verify(_ => _.SortedSetRangeByScore("prefix:key", 1.23, 1.23, Exclude.Start, Order.Descending, 123, 456, CommandFlags.HighPriority));
        }

        [Fact]
        public void SortedSetRangeByScoreWithScores()
        {
            wrapper.SortedSetRangeByScoreWithScores("key", 1.23, 1.23, Exclude.Start, Order.Descending, 123, 456, CommandFlags.HighPriority);
            mock.Verify(_ => _.SortedSetRangeByScoreWithScores("prefix:key", 1.23, 1.23, Exclude.Start, Order.Descending, 123, 456, CommandFlags.HighPriority));
        }

        [Fact]
        public void SortedSetRangeByValue()
        {
            wrapper.SortedSetRangeByValue("key", "min", "max", Exclude.Start, 123, 456, CommandFlags.HighPriority);
            mock.Verify(_ => _.SortedSetRangeByValue("prefix:key", "min", "max", Exclude.Start, 123, 456, CommandFlags.HighPriority));
        }

        [Fact]
        public void SortedSetRank()
        {
            wrapper.SortedSetRank("key", "member", Order.Descending, CommandFlags.HighPriority);
            mock.Verify(_ => _.SortedSetRank("prefix:key", "member", Order.Descending, CommandFlags.HighPriority));
        }

        [Fact]
        public void SortedSetRemove_1()
        {
            wrapper.SortedSetRemove("key", "member", CommandFlags.HighPriority);
            mock.Verify(_ => _.SortedSetRemove("prefix:key", "member", CommandFlags.HighPriority));
        }

        [Fact]
        public void SortedSetRemove_2()
        {
            RedisValue[] members = new RedisValue[0];
            wrapper.SortedSetRemove("key", members, CommandFlags.HighPriority);
            mock.Verify(_ => _.SortedSetRemove("prefix:key", members, CommandFlags.HighPriority));
        }

        [Fact]
        public void SortedSetRemoveRangeByRank()
        {
            wrapper.SortedSetRemoveRangeByRank("key", 123, 456, CommandFlags.HighPriority);
            mock.Verify(_ => _.SortedSetRemoveRangeByRank("prefix:key", 123, 456, CommandFlags.HighPriority));
        }

        [Fact]
        public void SortedSetRemoveRangeByScore()
        {
            wrapper.SortedSetRemoveRangeByScore("key", 1.23, 1.23, Exclude.Start, CommandFlags.HighPriority);
            mock.Verify(_ => _.SortedSetRemoveRangeByScore("prefix:key", 1.23, 1.23, Exclude.Start, CommandFlags.HighPriority));
        }

        [Fact]
        public void SortedSetRemoveRangeByValue()
        {
            wrapper.SortedSetRemoveRangeByValue("key", "min", "max", Exclude.Start, CommandFlags.HighPriority);
            mock.Verify(_ => _.SortedSetRemoveRangeByValue("prefix:key", "min", "max", Exclude.Start, CommandFlags.HighPriority));
        }

        [Fact]
        public void SortedSetScan()
        {
            wrapper.SortedSetScan("key", "pattern", 123, flags: CommandFlags.HighPriority);
            mock.Verify(_ => _.SortedSetScan("prefix:key", "pattern", 123, 0, 0, CommandFlags.HighPriority));
        }

        [Fact]
        public void SortedSetScore()
        {
            wrapper.SortedSetScore("key", "member", CommandFlags.HighPriority);
            mock.Verify(_ => _.SortedSetScore("prefix:key", "member", CommandFlags.HighPriority));
        }

        [Fact]
        public void StreamAcknowledge_1()
        {
            wrapper.StreamAcknowledge("key", "group", "0-0", CommandFlags.HighPriority);
            mock.Verify(_ => _.StreamAcknowledge("prefix:key", "group", "0-0", CommandFlags.HighPriority));
        }

        [Fact]
        public void StreamAcknowledge_2()
        {
            var messageIds = new RedisValue[] { "0-0", "0-1", "0-2" };
            wrapper.StreamAcknowledge("key", "group", messageIds, CommandFlags.HighPriority);
            mock.Verify(_ => _.StreamAcknowledge("prefix:key", "group", messageIds, CommandFlags.HighPriority));
        }

        [Fact]
        public void StreamAdd_1()
        {
            wrapper.StreamAdd("key", "field1", "value1", "*", 1000, true, CommandFlags.HighPriority);
            mock.Verify(_ => _.StreamAdd("prefix:key", "field1", "value1", "*", 1000, true, CommandFlags.HighPriority));
        }

        [Fact]
        public void StreamAdd_2()
        {
            var fields = new NameValueEntry[0];
            wrapper.StreamAdd("key", fields, "*", 1000, true, CommandFlags.HighPriority);
            mock.Verify(_ => _.StreamAdd("prefix:key", fields, "*", 1000, true, CommandFlags.HighPriority));
        }

        [Fact]
        public void StreamClaimMessages()
        {
            var messageIds = new RedisValue[0];
            wrapper.StreamClaim("key", "group", "consumer", 1000, messageIds, CommandFlags.HighPriority);
            mock.Verify(_ => _.StreamClaim("prefix:key", "group", "consumer", 1000, messageIds, CommandFlags.HighPriority));
        }

        [Fact]
        public void StreamClaimMessagesReturningIds()
        {
            var messageIds = new RedisValue[0];
            wrapper.StreamClaimIdsOnly("key", "group", "consumer", 1000, messageIds, CommandFlags.HighPriority);
            mock.Verify(_ => _.StreamClaimIdsOnly("prefix:key", "group", "consumer", 1000, messageIds, CommandFlags.HighPriority));
        }

        [Fact]
        public void StreamConsumerGroupSetPosition()
        {
            wrapper.StreamConsumerGroupSetPosition("key", "group", Position.Beginning, CommandFlags.HighPriority);
            mock.Verify(_ => _.StreamConsumerGroupSetPosition("prefix:key", "group", Position.Beginning, CommandFlags.HighPriority));
        }

        [Fact]
        public void StreamConsumerInfoGet()
        {
            wrapper.StreamConsumerInfo("key", "group", CommandFlags.HighPriority);
            mock.Verify(_ => _.StreamConsumerInfo("prefix:key", "group", CommandFlags.HighPriority));
        }

        [Fact]
        public void StreamCreateConsumerGroup()
        {
            wrapper.StreamCreateConsumerGroup("key", "group", Position.Beginning, CommandFlags.HighPriority);
            mock.Verify(_ => _.StreamCreateConsumerGroup("prefix:key", "group", Position.Beginning, CommandFlags.HighPriority));
        }

        [Fact]
        public void StreamGroupInfoGet()
        {
            wrapper.StreamGroupInfo("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.StreamGroupInfo("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void StreamInfoGet()
        {
            wrapper.StreamInfo("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.StreamInfo("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void StreamLength()
        {
            wrapper.StreamLength("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.StreamLength("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void StreamMessagesDelete()
        {
            var messageIds = new RedisValue[0] { };
            wrapper.StreamDelete("key", messageIds, CommandFlags.HighPriority);
            mock.Verify(_ => _.StreamDelete("prefix:key", messageIds, CommandFlags.HighPriority));
        }

        [Fact]
        public void StreamDeleteConsumer()
        {
            wrapper.StreamDeleteConsumer("key", "group", "consumer", CommandFlags.HighPriority);
            mock.Verify(_ => _.StreamDeleteConsumer("prefix:key", "group", "consumer", CommandFlags.HighPriority));
        }

        [Fact]
        public void StreamDeleteConsumerGroup()
        {
            wrapper.StreamDeleteConsumerGroup("key", "group", CommandFlags.HighPriority);
            mock.Verify(_ => _.StreamDeleteConsumerGroup("prefix:key", "group", CommandFlags.HighPriority));
        }

        [Fact]
        public void StreamPendingInfoGet()
        {
            wrapper.StreamPending("key", "group", CommandFlags.HighPriority);
            mock.Verify(_ => _.StreamPending("prefix:key", "group", CommandFlags.HighPriority));
        }

        [Fact]
        public void StreamPendingMessageInfoGet()
        {
            wrapper.StreamPendingMessages("key", "group", 10, RedisValue.Null, "-", "+", CommandFlags.HighPriority);
            mock.Verify(_ => _.StreamPendingMessages("prefix:key", "group", 10, RedisValue.Null, "-", "+", CommandFlags.HighPriority));
        }

        [Fact]
        public void StreamRange()
        {
            wrapper.StreamRange("key", "-", "+", null, Order.Ascending, CommandFlags.HighPriority);
            mock.Verify(_ => _.StreamRange("prefix:key", "-", "+", null, Order.Ascending, CommandFlags.HighPriority));
        }

        [Fact]
        public void StreamRead_1()
        {
            var streamPositions = new StreamPosition[0] { };
            wrapper.StreamRead(streamPositions, null, CommandFlags.HighPriority);
            mock.Verify(_ => _.StreamRead(streamPositions, null, CommandFlags.HighPriority));
        }

        [Fact]
        public void StreamRead_2()
        {
            wrapper.StreamRead("key", new Position("0-0"), null, CommandFlags.HighPriority);
            mock.Verify(_ => _.StreamRead("prefix:key", new Position("0-0"), null, CommandFlags.HighPriority));
        }

        [Fact]
        public void StreamStreamReadGroup_1()
        {
            wrapper.StreamReadGroup("key", "group", "consumer", new Position("0-0"), 10, CommandFlags.HighPriority);
            mock.Verify(_ => _.StreamReadGroup("prefix:key", "group", "consumer", new Position("0-0"), 10, CommandFlags.HighPriority));
        }

        [Fact]
        public void StreamStreamReadGroup_2()
        {
            var streamPositions = new StreamPosition[0] { };
            wrapper.StreamReadGroup(streamPositions, "group", "consumer", 10, CommandFlags.HighPriority);
            mock.Verify(_ => _.StreamReadGroup(streamPositions, "group", "consumer", 10, CommandFlags.HighPriority));
        }

        [Fact]
        public void StreamTrim()
        {
            wrapper.StreamTrim("key", 1000, true, CommandFlags.HighPriority);
            mock.Verify(_ => _.StreamTrim("prefix:key", 1000, true, CommandFlags.HighPriority));
        }

        [Fact]
        public void StringAppend()
        {
            wrapper.StringAppend("key", "value", CommandFlags.HighPriority);
            mock.Verify(_ => _.StringAppend("prefix:key", "value", CommandFlags.HighPriority));
        }

        [Fact]
        public void StringBitCount()
        {
            wrapper.StringBitCount("key", 123, 456, CommandFlags.HighPriority);
            mock.Verify(_ => _.StringBitCount("prefix:key", 123, 456, CommandFlags.HighPriority));
        }

        [Fact]
        public void StringBitOperation_1()
        {
            wrapper.StringBitOperation(Bitwise.Xor, "destination", "first", "second", CommandFlags.HighPriority);
            mock.Verify(_ => _.StringBitOperation(Bitwise.Xor, "prefix:destination", "prefix:first", "prefix:second", CommandFlags.HighPriority));
        }

        [Fact]
        public void StringBitOperation_2()
        {
            RedisKey[] keys = new RedisKey[] { "a", "b" };
            Expression<Func<RedisKey[], bool>> valid = _ => _.Length == 2 && _[0] == "prefix:a" && _[1] == "prefix:b";
            wrapper.StringBitOperation(Bitwise.Xor, "destination", keys, CommandFlags.HighPriority);
            mock.Verify(_ => _.StringBitOperation(Bitwise.Xor, "prefix:destination", It.Is(valid), CommandFlags.HighPriority));
        }

        [Fact]
        public void StringBitPosition()
        {
            wrapper.StringBitPosition("key", true, 123, 456, CommandFlags.HighPriority);
            mock.Verify(_ => _.StringBitPosition("prefix:key", true, 123, 456, CommandFlags.HighPriority));
        }

        [Fact]
        public void StringDecrement_1()
        {
            wrapper.StringDecrement("key", 123, CommandFlags.HighPriority);
            mock.Verify(_ => _.StringDecrement("prefix:key", 123, CommandFlags.HighPriority));
        }

        [Fact]
        public void StringDecrement_2()
        {
            wrapper.StringDecrement("key", 1.23, CommandFlags.HighPriority);
            mock.Verify(_ => _.StringDecrement("prefix:key", 1.23, CommandFlags.HighPriority));
        }

        [Fact]
        public void StringGet_1()
        {
            wrapper.StringGet("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.StringGet("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void StringGet_2()
        {
            RedisKey[] keys = new RedisKey[] { "a", "b" };
            Expression<Func<RedisKey[], bool>> valid = _ => _.Length == 2 && _[0] == "prefix:a" && _[1] == "prefix:b";
            wrapper.StringGet(keys, CommandFlags.HighPriority);
            mock.Verify(_ => _.StringGet(It.Is(valid), CommandFlags.HighPriority));
        }

        [Fact]
        public void StringGetBit()
        {
            wrapper.StringGetBit("key", 123, CommandFlags.HighPriority);
            mock.Verify(_ => _.StringGetBit("prefix:key", 123, CommandFlags.HighPriority));
        }

        [Fact]
        public void StringGetRange()
        {
            wrapper.StringGetRange("key", 123, 456, CommandFlags.HighPriority);
            mock.Verify(_ => _.StringGetRange("prefix:key", 123, 456, CommandFlags.HighPriority));
        }

        [Fact]
        public void StringGetSet()
        {
            wrapper.StringGetSet("key", "value", CommandFlags.HighPriority);
            mock.Verify(_ => _.StringGetSet("prefix:key", "value", CommandFlags.HighPriority));
        }

        [Fact]
        public void StringGetWithExpiry()
        {
            wrapper.StringGetWithExpiry("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.StringGetWithExpiry("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void StringIncrement_1()
        {
            wrapper.StringIncrement("key", 123, CommandFlags.HighPriority);
            mock.Verify(_ => _.StringIncrement("prefix:key", 123, CommandFlags.HighPriority));
        }

        [Fact]
        public void StringIncrement_2()
        {
            wrapper.StringIncrement("key", 1.23, CommandFlags.HighPriority);
            mock.Verify(_ => _.StringIncrement("prefix:key", 1.23, CommandFlags.HighPriority));
        }

        [Fact]
        public void StringLength()
        {
            wrapper.StringLength("key", CommandFlags.HighPriority);
            mock.Verify(_ => _.StringLength("prefix:key", CommandFlags.HighPriority));
        }

        [Fact]
        public void StringSet_1()
        {
            TimeSpan expiry = TimeSpan.FromSeconds(123);
            wrapper.StringSet("key", "value", expiry, When.Exists, CommandFlags.HighPriority);
            mock.Verify(_ => _.StringSet("prefix:key", "value", expiry, When.Exists, CommandFlags.HighPriority));
        }

        [Fact]
        public void StringSet_2()
        {
            KeyValuePair<RedisKey, RedisValue>[] values = new KeyValuePair<RedisKey, RedisValue>[] { new KeyValuePair<RedisKey, RedisValue>("a", "x"), new KeyValuePair<RedisKey, RedisValue>("b", "y") };
            Expression<Func<KeyValuePair<RedisKey, RedisValue>[], bool>> valid = _ => _.Length == 2 && _[0].Key == "prefix:a" && _[0].Value == "x" && _[1].Key == "prefix:b" && _[1].Value == "y";
            wrapper.StringSet(values, When.Exists, CommandFlags.HighPriority);
            mock.Verify(_ => _.StringSet(It.Is(valid), When.Exists, CommandFlags.HighPriority));
        }

        [Fact]
        public void StringSetBit()
        {
            wrapper.StringSetBit("key", 123, true, CommandFlags.HighPriority);
            mock.Verify(_ => _.StringSetBit("prefix:key", 123, true, CommandFlags.HighPriority));
        }

        [Fact]
        public void StringSetRange()
        {
            wrapper.StringSetRange("key", 123, "value", CommandFlags.HighPriority);
            mock.Verify(_ => _.StringSetRange("prefix:key", 123, "value", CommandFlags.HighPriority));
        }
    }
}
