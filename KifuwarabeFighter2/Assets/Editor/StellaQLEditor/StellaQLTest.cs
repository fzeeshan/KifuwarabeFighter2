﻿using NUnit.Framework;
using StellaQL;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StellaQLTest {

    static StellaQLTest()
    {
        StellaQLTest.InstanceTable = new StateExTable();
    }

    /// <summary>
    /// アニメーターのステート
    /// </summary>
    public class StateExRecord : AbstractStateExRecord
    {
        public static StateExRecord Build(string fullpath, StateExTable.Attr attribute)
        {
            return new StateExRecord(fullpath, Animator.StringToHash(fullpath), attribute);
        }
        public StateExRecord(string fullpath, int fullpathHash, StateExTable.Attr attribute):base(fullpath, fullpathHash, (int)attribute)
        {
        }

        public override bool HasFlag_attr(int enumration)
        {
            return ((StateExTable.Attr)this.AttributeEnum).HasFlag((StateExTable.Attr)enumration);
        }
    }

    public static StateExTable InstanceTable;
    public class StateExTable : AbstractStateExTable
    {
        [Flags]
        public enum Attr
        {
            Zero = 0, // (0) 別に 0 は無くてもいい。
            Alpha = 1, // (1)
            Beta = 1 << 1, // (2)
            Cee = 1 << 2, // (4)
            Dee = 1 << 3, // (8)
            Eee = 1 << 4, // (16)
        }

        public const string FULLPATH_ALPACA = "Base Layer.StellaQL Practice.Alpaca";
        public const string FULLPATH_BEAR = "Base Layer.StellaQL Practice.Bear";
        public const string FULLPATH_CAT = "Base Layer.StellaQL Practice.Cat";
        public const string FULLPATH_DOG = "Base Layer.StellaQL Practice.Dog";
        public const string FULLPATH_ELEPHANT = "Base Layer.StellaQL Practice.Elephant";
        public const string FULLPATH_FOX = "Base Layer.StellaQL Practice.Fox";
        public const string FULLPATH_GIRAFFE = "Base Layer.StellaQL Practice.Giraffe";
        public const string FULLPATH_HORSE = "Base Layer.StellaQL Practice.Horse";
        public const string FULLPATH_IGUANA = "Base Layer.StellaQL Practice.Iguana";
        public const string FULLPATH_JELLYFISH = "Base Layer.StellaQL Practice.Jellyfish";
        public const string FULLPATH_KANGAROO = "Base Layer.StellaQL Practice.Kangaroo";
        public const string FULLPATH_LION = "Base Layer.StellaQL Practice.Lion";
        public const string FULLPATH_MONKEY = "Base Layer.StellaQL Practice.Monkey";
        public const string FULLPATH_NUTRIA = "Base Layer.StellaQL Practice.Nutria";
        public const string FULLPATH_OX = "Base Layer.StellaQL Practice.Ox";
        public const string FULLPATH_PIG = "Base Layer.StellaQL Practice.Pig";
        public const string FULLPATH_QUETZAL = "Base Layer.StellaQL Practice.Quetzal";
        public const string FULLPATH_RABBIT = "Base Layer.StellaQL Practice.Rabbit";
        public const string FULLPATH_SHEEP = "Base Layer.StellaQL Practice.Sheep";
        public const string FULLPATH_TIGER = "Base Layer.StellaQL Practice.Tiger";
        public const string FULLPATH_UNICORN = "Base Layer.StellaQL Practice.Unicorn";
        public const string FULLPATH_VIXEN = "Base Layer.StellaQL Practice.Vixen";
        public const string FULLPATH_WOLF = "Base Layer.StellaQL Practice.Wolf";
        public const string FULLPATH_XENOPUS = "Base Layer.StellaQL Practice.Xenopus";
        public const string FULLPATH_YAK = "Base Layer.StellaQL Practice.Yak";
        public const string FULLPATH_ZEBRA = "Base Layer.StellaQL Practice.Zebra";

        public StateExTable()
        {
            List<StateExRecordable> temp = new List<StateExRecordable>()
            {
                StateExRecord.Build(  FULLPATH_ALPACA, Attr.Alpha | Attr.Cee),// {E}(1) AC(1) ([(A C)(B)]{E})(1)
                StateExRecord.Build(  FULLPATH_BEAR, Attr.Alpha | Attr.Beta | Attr.Eee),// B(1) AE(1) AE,B,E(1)
                StateExRecord.Build(  FULLPATH_CAT, Attr.Alpha | Attr.Cee),// {E}(2) AC(2) ([(A C)(B)]{E})(2)
                StateExRecord.Build(  FULLPATH_DOG, Attr.Dee),// {E}(3)
                StateExRecord.Build(  FULLPATH_ELEPHANT, Attr.Alpha | Attr.Eee),//AE(2) AE,B,E(2) Nn(1)
                StateExRecord.Build(  FULLPATH_FOX, Attr.Zero),// {E}(4)
                StateExRecord.Build(  FULLPATH_GIRAFFE, Attr.Alpha | Attr.Eee),//AE(3) AE,B,E(3)
                StateExRecord.Build(  FULLPATH_HORSE, Attr.Eee),// AE,B,E(4)
                StateExRecord.Build(  FULLPATH_IGUANA, Attr.Alpha),// {E}(5) Nn(2)
                StateExRecord.Build(  FULLPATH_JELLYFISH, Attr.Eee),// AE,B,E(5)
                StateExRecord.Build(  FULLPATH_KANGAROO, Attr.Alpha),// {E}(6) Nn(3)
                StateExRecord.Build(  FULLPATH_LION, Attr.Zero),// {E}(7) Nn(4)
                StateExRecord.Build(  FULLPATH_MONKEY, Attr.Eee),// AE,B,E(6) Nn(5)
                StateExRecord.Build(  FULLPATH_NUTRIA, Attr.Alpha),// {E}(8) Nn(6)
                StateExRecord.Build(  FULLPATH_OX, Attr.Zero),// {E}(9)
                StateExRecord.Build(  FULLPATH_PIG, Attr.Zero),// {E}(10)
                StateExRecord.Build(  FULLPATH_QUETZAL, Attr.Alpha | Attr.Eee),//AE(4) AE,B,E(7)
                StateExRecord.Build(  FULLPATH_RABBIT, Attr.Alpha | Attr.Beta),// {E}(11) B(2) ([(A C)(B)]{E})(3)  AE,B,E(8)
                StateExRecord.Build(  FULLPATH_SHEEP, Attr.Eee),// AE,B,E(9)
                StateExRecord.Build(  FULLPATH_TIGER, Attr.Eee),// AE,B,E(10)
                StateExRecord.Build(  FULLPATH_UNICORN, Attr.Cee),// {E}(12) Nn(7)
                StateExRecord.Build(  FULLPATH_VIXEN, Attr.Eee),// AE,B,E(11) Nn(8)
                StateExRecord.Build(  FULLPATH_WOLF, Attr.Zero),// {E}(13)
                StateExRecord.Build(  FULLPATH_XENOPUS, Attr.Eee),// AE,B,E(12) Nn(9)
                StateExRecord.Build(  FULLPATH_YAK, Attr.Alpha),// {E}(14)
                StateExRecord.Build(  FULLPATH_ZEBRA, Attr.Alpha | Attr.Beta | Attr.Eee),// B(3) AE(5) AE,B,E(13)
            };

            hash_to_exRecord = new Dictionary<int, StateExRecordable>();
            foreach (StateExRecordable record in temp) { hash_to_exRecord.Add(record.FullPathHash, record); }
        }
    }

    #region N30 Query (文字列を与えて、レコード・インデックスを取ってくる)
    [Test]
    public void N30_Query_StateSelect()
    {
        string query = @"STATE SELECT
                        WHERE ATTR ([(Alpha Cee)(Beta)]{Eee})";
        HashSet<int> recordHashes;
        bool successful = Querier.ExecuteStateSelect(query, typeof(StateExTable.Attr), InstanceTable.hash_to_exRecord, out recordHashes);

        Assert.AreEqual(3, recordHashes.Count);
        Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_ALPACA)));
        Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_CAT)));
        Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_RABBIT)));
    }

    /// <summary>
    /// コメントと空行のテスト
    /// </summary>
    [Test]
    public void N30_Query_Comment()
    {
        string query = @"# コメントＡ

                        STATE SELECT

                        # コメントＢ

                        WHERE ATTR ([(Alpha Cee)(Beta)]{Eee})

                        # コメントＣ

                        ";
        // STATE SELECT文が動けば OK☆
        HashSet<int> recordHashes;
        bool successful = Querier.ExecuteStateSelect(query, typeof(StateExTable.Attr), InstanceTable.hash_to_exRecord, out recordHashes);

        Assert.AreEqual(3, recordHashes.Count);
        Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_ALPACA)));
        Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_CAT)));
        Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_RABBIT)));
    }

    [Test]
    public void N30_Query_TransitionSelect()
    {
        string query = @"TRANSITION SELECT
                        FROM ""Base Layer\.StellaQL Practice\.Zebra""
                        TO ATTR ([(Alpha Cee)(Beta)]{Eee})";
        HashSet<int> recordHashesSrc;
        HashSet<int> recordHashesDst;
        bool successful = Querier.ExecuteTransitionSelect(query, typeof(StateExTable.Attr), InstanceTable.hash_to_exRecord, out recordHashesSrc, out recordHashesDst);

        Assert.AreEqual(1, recordHashesSrc.Count);
        Assert.IsTrue(recordHashesSrc.Contains(Animator.StringToHash(StateExTable.FULLPATH_ZEBRA)));
        Assert.AreEqual(3, recordHashesDst.Count);
        Assert.IsTrue(recordHashesDst.Contains(Animator.StringToHash(StateExTable.FULLPATH_ALPACA)));
        Assert.IsTrue(recordHashesDst.Contains(Animator.StringToHash(StateExTable.FULLPATH_CAT)));
        Assert.IsTrue(recordHashesDst.Contains(Animator.StringToHash(StateExTable.FULLPATH_RABBIT)));
    }
    #endregion

    #region N40 Fetch (レコード・インデックスを取ってくる)
    /// <summary>
    /// ロッカーを元に、レコード・インデックスを取得
    /// </summary>
    [Test]
    public void N40_Fetch_ByLockers()
    {
        List<List<string>> tokenLockers = new List<List<string>>(){ // 元は "([(Alpha Cee)(Beta)]{Eee})"
            new List<string>() { "Cee", "Alpha", },
            new List<string>() { "Beta", },
            new List<string>() { "1","0",},
            new List<string>() { "Eee", },
            new List<string>() { "3","2",},
        };
        List<string> tokenLockersOperation = new List<string>() { "(", "(", "[", "{", "(", };

        List<HashSet<int>> recordHashesLockers;
        Fetcher.TokenLockers_to_recordHashesLockers(
            tokenLockers, tokenLockersOperation, typeof(StateExTable.Attr), InstanceTable.hash_to_exRecord, out recordHashesLockers);

        Assert.AreEqual(5, recordHashesLockers.Count);
        Assert.AreEqual(2, recordHashesLockers[0].Count);
        Assert.IsTrue(recordHashesLockers[0].Contains(Animator.StringToHash(StateExTable.FULLPATH_ALPACA)));
        Assert.IsTrue(recordHashesLockers[0].Contains(Animator.StringToHash(StateExTable.FULLPATH_CAT)));
        Assert.AreEqual(3, recordHashesLockers[1].Count);
        Assert.IsTrue(recordHashesLockers[1].Contains(Animator.StringToHash(StateExTable.FULLPATH_BEAR)));
        Assert.IsTrue(recordHashesLockers[1].Contains(Animator.StringToHash(StateExTable.FULLPATH_RABBIT)));
        Assert.IsTrue(recordHashesLockers[1].Contains(Animator.StringToHash(StateExTable.FULLPATH_ZEBRA)));
        Assert.AreEqual(5, recordHashesLockers[2].Count);
        Assert.IsTrue(recordHashesLockers[2].Contains(Animator.StringToHash(StateExTable.FULLPATH_ALPACA)));
        Assert.IsTrue(recordHashesLockers[2].Contains(Animator.StringToHash(StateExTable.FULLPATH_CAT)));
        Assert.IsTrue(recordHashesLockers[2].Contains(Animator.StringToHash(StateExTable.FULLPATH_BEAR)));
        Assert.IsTrue(recordHashesLockers[2].Contains(Animator.StringToHash(StateExTable.FULLPATH_RABBIT)));
        Assert.IsTrue(recordHashesLockers[2].Contains(Animator.StringToHash(StateExTable.FULLPATH_ZEBRA)));
        Assert.AreEqual(14, recordHashesLockers[3].Count);
        Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(StateExTable.FULLPATH_ALPACA)));
        Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(StateExTable.FULLPATH_CAT)));
        Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(StateExTable.FULLPATH_DOG)));
        Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(StateExTable.FULLPATH_FOX)));
        Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(StateExTable.FULLPATH_IGUANA)));
        Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(StateExTable.FULLPATH_KANGAROO)));
        Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(StateExTable.FULLPATH_LION)));
        Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(StateExTable.FULLPATH_NUTRIA)));
        Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(StateExTable.FULLPATH_OX)));
        Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(StateExTable.FULLPATH_PIG)));
        Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(StateExTable.FULLPATH_RABBIT)));
        Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(StateExTable.FULLPATH_UNICORN)));
        Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(StateExTable.FULLPATH_WOLF)));
        Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(StateExTable.FULLPATH_YAK)));
        Assert.AreEqual(3, recordHashesLockers[4].Count);
        Assert.IsTrue(recordHashesLockers[4].Contains(Animator.StringToHash(StateExTable.FULLPATH_ALPACA)));
        Assert.IsTrue(recordHashesLockers[4].Contains(Animator.StringToHash(StateExTable.FULLPATH_CAT)));
        Assert.IsTrue(recordHashesLockers[4].Contains(Animator.StringToHash(StateExTable.FULLPATH_RABBIT)));
    }
    #endregion

    #region N50 element set (要素集合)
    /// <summary>
    /// （　）要素フィルター
    /// </summary>
    [Test]
    public void N50_RecordHashes_FilteringElementsAnd()
    {
        // 条件は　「 Bear, Elephant 」 AND 「 Bear, Giraffe 」
        HashSet<int> lockerNumbers = new HashSet<int>() { 0, 1 };
        List<HashSet<int>> reordIndexLockers = new List<HashSet<int>>()
        {
            new HashSet<int>() { Animator.StringToHash(StateExTable.FULLPATH_BEAR),Animator.StringToHash(StateExTable.FULLPATH_ELEPHANT) },
            new HashSet<int>() { Animator.StringToHash(StateExTable.FULLPATH_BEAR), Animator.StringToHash(StateExTable.FULLPATH_GIRAFFE) },
        };
        HashSet<int> recordHashes = ElementSet.RecordHashes_FilteringElementsAnd(lockerNumbers, reordIndexLockers);

        // 結果は　Bear
        Assert.AreEqual(1, recordHashes.Count);
        if (1 == recordHashes.Count)
        {
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_BEAR)));
        }
    }

    /// <summary>
    /// [ ] 要素フィルター
    /// </summary>
    [Test]
    public void N50_RecordHashes_FilteringElementsOr()
    {
        // 条件は　「 Bear, Elephant 」 OR 「 Bear, Giraffe 」
        HashSet<int> lockerNumbers = new HashSet<int>() { 0, 1 };
        List<HashSet<int>> recordHasheslockers = new List<HashSet<int>>()
        {
            new HashSet<int>() { Animator.StringToHash(StateExTable.FULLPATH_BEAR),Animator.StringToHash(StateExTable.FULLPATH_ELEPHANT) },
            new HashSet<int>() { Animator.StringToHash(StateExTable.FULLPATH_BEAR),Animator.StringToHash(StateExTable.FULLPATH_GIRAFFE) },
        };
        HashSet<int> recordHashes = ElementSet.RecordHashes_FilteringElementsOr(lockerNumbers, recordHasheslockers);

        // 結果は　Bear, Elephant, Giraffe
        Assert.AreEqual(3, recordHashes.Count);
        if (3 == recordHashes.Count)
        {
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_BEAR)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_ELEPHANT)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_GIRAFFE)));
        }
    }

    /// <summary>
    /// { } 要素フィルター
    /// </summary>
    [Test]
    public void N50_RecordHashes_FilteringElementsNotAndNot()
    {
        // 条件は　NOT「 Bear, Elephant 」 AND NOT「 Bear, Giraffe 」
        HashSet<int> lockerNumbers = new HashSet<int>() { 0, 1 };
        List<HashSet<int>> recordHashesLockers = new List<HashSet<int>>()
        {
            new HashSet<int>() { Animator.StringToHash(StateExTable.FULLPATH_BEAR),Animator.StringToHash(StateExTable.FULLPATH_ELEPHANT) },
            new HashSet<int>() { Animator.StringToHash(StateExTable.FULLPATH_BEAR),Animator.StringToHash(StateExTable.FULLPATH_GIRAFFE) },
        };
        HashSet<int> recordHashes = ElementSet.RecordHashes_FilteringElementsNotAndNot(lockerNumbers, recordHashesLockers, InstanceTable.hash_to_exRecord);

        // 結果は　Alpaca,Cat,Dog,Fox,Horse,Iguana,Jellyfish,Kangaroo,Lion,Monkey,Nutria,Ox,Pig,Quetzal,Rabbit,Sheep,Tiger,Unicorn,Vixen,Wolf,Xenopus,Yak,Zebra
        Assert.AreEqual(23, recordHashes.Count);
        if (23 == recordHashes.Count)
        {
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_ALPACA)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_CAT)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_DOG)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_FOX)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_HORSE)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_IGUANA)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_JELLYFISH)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_KANGAROO)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_LION)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_MONKEY)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_NUTRIA)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_OX)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_PIG)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_QUETZAL)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_RABBIT)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_SHEEP)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_TIGER)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_UNICORN)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_VIXEN)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_WOLF)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_XENOPUS)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_YAK)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_ZEBRA)));
        }
    }

    /// <summary>
    /// ステート名正規表現フィルター
    /// </summary>
    [Test]
    public void N50_RecordHashes_FilteringStateFullNameRegex()
    {
        // 条件は、「Base Layer.」の下に、n または N が含まれるもの
        string pattern = @"Base Layer\.StellaQL Practice\.\w*[Nn]\w*";
        HashSet<int> recordHashes = ElementSet.RecordHashes_FilteringStateFullNameRegex(pattern, InstanceTable.hash_to_exRecord);

        // 結果は　Elephant、Iguana、Kangaroo、Lion、Monkey、Nutria、Unicorn、Vixen、Xenopus
        Assert.AreEqual(9, recordHashes.Count);
        if (9 == recordHashes.Count)
        {
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_ELEPHANT)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_IGUANA)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_KANGAROO)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_LION)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_MONKEY)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_NUTRIA)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_UNICORN)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_VIXEN)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_XENOPUS)));
        }
    }

    /// <summary>
    /// （　）属性フィルター
    /// </summary>
    [Test]
    public void N50_RecordHashes_FilteringAttributesAnd()
    {
        // 条件は　Alpha | Eee
        HashSet<int> attrs = new HashSet<int>() { (int)StateExTable.Attr.Alpha | (int)StateExTable.Attr.Eee };
        HashSet<int> recordHashes = ElementSet.RecordHashes_FilteringAttributesAnd(attrs, InstanceTable.hash_to_exRecord);

        // 結果は　Bear、Elephant、Giraffe、Quetzal、Zebra
        Assert.AreEqual(5, recordHashes.Count);
        if (5 == recordHashes.Count)
        {
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_BEAR)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_ELEPHANT)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_GIRAFFE)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_QUETZAL)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_ZEBRA)));
        }
    }

    /// <summary>
    /// [　] 属性フィルター
    /// </summary>
    [Test]
    public void N50_RecordHashes_FilteringAttributesOr()
    {
        // 条件は　（Alpha | Eee）、Beta、Eee
        HashSet<int> attrs = new HashSet<int>() { (int)StateExTable.Attr.Alpha | (int)StateExTable.Attr.Eee, (int)StateExTable.Attr.Beta, (int)StateExTable.Attr.Eee };
        HashSet<int> recordHashes = ElementSet.RecordHashes_FilteringAttributesOr(attrs, InstanceTable.hash_to_exRecord);

        // 結果は　Bear、Elephant、Giraffe、Horse、Jellyfish、Monkey、Quetzal、Rabbit、Sheep、Tiger、Vixen、Xenopus、Zebra
        Assert.AreEqual(13, recordHashes.Count);
        if (13 == recordHashes.Count)
        {
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_BEAR)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_ELEPHANT)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_GIRAFFE)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_HORSE)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_JELLYFISH)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_MONKEY)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_QUETZAL)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_RABBIT)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_SHEEP)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_TIGER)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_VIXEN)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_XENOPUS)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_ZEBRA)));
        }
    }

    /// <summary>
    /// ｛　｝ 属性フィルター
    /// </summary>
    [Test]
    public void N50_RecordHashes_FilteringAttributesNotAndNot()
    {
        // 条件は　（Alpha | Eee）、Beta、Eee
        HashSet<int> attrs = new HashSet<int>() { (int)StateExTable.Attr.Alpha | (int)StateExTable.Attr.Eee, (int)StateExTable.Attr.Beta, (int)StateExTable.Attr.Eee };
        HashSet<int> recordHashes = ElementSet.RecordHashes_FilteringAttributesNotAndNot(attrs, InstanceTable.hash_to_exRecord);

        // 結果は　Alpaca、Cat、Dog、Fox、Iguana、Kangaroo、Lion、Nutria、Ox、Pig、Unicorn、Wolf、Yak
        Assert.AreEqual(13, recordHashes.Count);
        if (13 == recordHashes.Count)
        {
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_ALPACA)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_CAT)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_DOG)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_FOX)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_IGUANA)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_KANGAROO)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_LION)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_NUTRIA)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_OX)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_PIG)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_UNICORN)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_WOLF)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(StateExTable.FULLPATH_YAK)));
        }
    }
    #endregion

    #region N60 attribute set (属性集合)
    /// <summary>
    /// (　) で属性集合
    /// </summary>
    [Test]
    public void N60_ToAttrLocker_FromKeywordSet()
    {
        HashSet<int> set = new HashSet<int>() { (int)StateExTable.Attr.Beta, (int)StateExTable.Attr.Dee };
        HashSet<int> attrLocker = AttrSet.KeywordSet_to_attrLocker(set);//, typeof(AstateDatabase.Attr)

        //{ int i = 0; foreach (int attr in attrLocker) { Debug.Log("Attr[" + i + "]: " + (AstateDatabase.Attr)attr + " (" + attr + ")"); i++; } }

        Assert.AreEqual(1, attrLocker.Count);
        if (1 == attrLocker.Count)
        {
            Assert.IsTrue( attrLocker.Contains((int)StateExTable.Attr.Beta | (int)StateExTable.Attr.Dee));
        }
    }

    /// <summary>
    /// [　] で属性集合
    /// </summary>
    [Test]
    public void N60_ToAttrLocker_FromKeywordlistSet()
    {
        HashSet<int> set = new HashSet<int>() { (int)StateExTable.Attr.Beta, (int)StateExTable.Attr.Dee };
        HashSet<int> attrLocker = AttrSet.KeywordlistSet_to_attrLocker(set);

        // { int i = 0; foreach (int attr in attrLocker) { Debug.Log("Attr[" + i + "]: " + (AstateDatabase.Attr)attr + " (" + attr + ")"); i++; } }

        Assert.AreEqual(2, attrLocker.Count);
        if (2 == attrLocker.Count)
        {
            Assert.IsTrue( attrLocker.Contains((int)StateExTable.Attr.Beta));
            Assert.IsTrue( attrLocker.Contains((int)StateExTable.Attr.Dee));
        }
    }

    /// <summary>
    /// {　} で属性集合
    /// </summary>
    [Test]
    public void N60_ToAttrLocker_FromNGKeywordSet()
    {
        HashSet<int> set = new HashSet<int>() { (int)StateExTable.Attr.Beta, (int)StateExTable.Attr.Dee };
        HashSet<int> attrLocker = AttrSet.NGKeywordSet_to_attrLocker(set, typeof(StateExTable.Attr));

        // { int i = 0; foreach (int attr in attrLocker) { Debug.Log("Attr[" + i + "]: " + (AstateDatabase.Attr)attr + " (" + attr + ")"); i++; } }

        Assert.AreEqual(4, attrLocker.Count);
        if (4 == attrLocker.Count)
        {
            Assert.IsTrue( attrLocker.Contains((int)StateExTable.Attr.Zero));
            Assert.IsTrue( attrLocker.Contains((int)StateExTable.Attr.Alpha));
            Assert.IsTrue( attrLocker.Contains((int)StateExTable.Attr.Cee));
            Assert.IsTrue( attrLocker.Contains((int)StateExTable.Attr.Eee));
        }
    }

    /// <summary>
    /// 補集合を取れるかテスト
    /// </summary>
    [Test]
    public void N60_ToAttrLocker_GetComplement()
    {
        HashSet<int> set = new HashSet<int>() { (int)StateExTable.Attr.Beta, (int)StateExTable.Attr.Dee }; // int型にして持つ
        HashSet<int> attrLocker = AttrSet.Complement(set, typeof(StateExTable.Attr));

        // { int i = 0; foreach (int attr in attrLocker) { Debug.Log("Attr[" + i + "]: " + (AstateDatabase.Attr)attr + " (" + attr + ")"); i++; } }

        Assert.AreEqual(4, attrLocker.Count);
        if (4 == attrLocker.Count)
        {
            Assert.IsTrue( attrLocker.Contains((int)StateExTable.Attr.Zero));
            Assert.IsTrue( attrLocker.Contains((int)StateExTable.Attr.Alpha));
            Assert.IsTrue( attrLocker.Contains((int)StateExTable.Attr.Cee));
            Assert.IsTrue( attrLocker.Contains((int)StateExTable.Attr.Eee));
        }
    }
    #endregion

    #region N65 data builder (データ・ビルダー)
    /// <summary>
    /// ATTR部を解析（２）
    /// </summary>
    [Test]
    public void N65_Parse_AttrParentesis_TokensToLockers()
    {
        List<string> tokens = new List<string>() { "(", "[", "(", "Alpaca", "Bear", ")", "(", "Cat", "Dog", ")", "]", "{", "Elephant", "}", ")", };
        List<List<string>> tokenLockers;
        List<string> tokenLockersOperation;
        Querier.Tokens_to_lockers(tokens, out tokenLockers, out tokenLockersOperation);

        Assert.AreEqual(5, tokenLockers.Count);
        Assert.AreEqual(5, tokenLockersOperation.Count);
        //{ int i = 0; foreach (string token in lockers[0]) { Debug.Log("[0][" + i + "]=" + token); i++; } }
        Assert.AreEqual(2, tokenLockers[0].Count);
        //{ int i = 0; foreach (string token in lockers[1]) { Debug.Log("[1][" + i + "]=" + token); i++; } }
        Assert.AreEqual(2, tokenLockers[1].Count);
        //{ int i = 0; foreach (string token in lockers[2]) { Debug.Log("[2][" + i + "]=" + token); i++; } }
        Assert.AreEqual(2, tokenLockers[2].Count);
        //{ int i = 0; foreach (string token in lockers[3]) { Debug.Log("[3][" + i + "]=" + token); i++; } }
        Assert.AreEqual(1, tokenLockers[3].Count);
        //{ int i = 0; foreach (string token in lockers[4]) { Debug.Log("[4][" + i + "]=" + token); i++; } }
        Assert.AreEqual(2, tokenLockers[4].Count);
        Assert.AreEqual("Bear", tokenLockers[0][0]);
        Assert.AreEqual("Alpaca", tokenLockers[0][1]);
        Assert.AreEqual("(", tokenLockersOperation[0]);
        Assert.AreEqual("Dog", tokenLockers[1][0]);
        Assert.AreEqual("Cat", tokenLockers[1][1]);
        Assert.AreEqual("(", tokenLockersOperation[1]);
        Assert.AreEqual("1", tokenLockers[2][0]);
        Assert.AreEqual("0", tokenLockers[2][1]);
        Assert.AreEqual("[", tokenLockersOperation[2]);
        Assert.AreEqual("Elephant", tokenLockers[3][0]);
        Assert.AreEqual("{", tokenLockersOperation[3]);
        Assert.AreEqual("3", tokenLockers[4][0]);
        Assert.AreEqual("2", tokenLockers[4][1]);
        Assert.AreEqual("(", tokenLockersOperation[4]);
    }
    #endregion

    #region N70 syntax parser (構文パーサー)
    /// <summary>
    /// 構文解析 SET 句
    /// </summary>
    [Test]
    public void N70_ParsePhrase_AfterSet()
    {
        string phrase = @"Duration 0 ExitTime 1";
        Dictionary<string, string> properties = new Dictionary<string, string>();
        int caret = 0;
        bool successful = SyntaxP.ParsePhrase_AfterSet(phrase, ref caret, QueryTokens.WHERE, properties);

        Assert.IsTrue(successful);
        Assert.AreEqual(2, properties.Count);
        Assert.IsTrue(properties.ContainsKey("Duration"));
        Assert.AreEqual("0", properties["Duration"]);
        Assert.IsTrue(properties.ContainsKey("ExitTime"));
        Assert.AreEqual("1", properties["ExitTime"]);
    }

    /// <summary>
    /// 構文解析 SET 句
    /// </summary>
    [Test]
    public void N70_ParsePhrase_AfterSet_Stringliteral()
    {
        string phrase = @"tag ""hello""";
        Dictionary<string, string> properties = new Dictionary<string, string>();
        int caret = 0;
        bool successful = SyntaxP.ParsePhrase_AfterSet(phrase, ref caret, QueryTokens.WHERE, properties);

        Assert.IsTrue(successful);
        Assert.AreEqual(1, properties.Count);
        Assert.IsTrue(properties.ContainsKey("tag"));
        Assert.AreEqual("hello", properties["tag"]);
    }

    /// <summary>
    /// 構文解析 STATE SELECT 文
    /// </summary>
    [Test]
    public void N70_ParseStatement_StateSelect()
    {
        string query = @"STATE SELECT
                        WHERE ATTR ([(Alpha Cee)(Beta)]{Eee})";
        QueryTokens sq;
        bool successful = SyntaxP.ParseStatement_StateSelect(query, out sq);

        Assert.IsTrue(successful);
        Assert.AreEqual(QueryTokens.STATE, sq.Target);
        Assert.AreEqual(QueryTokens.SELECT, sq.Manipulation);
        Assert.AreEqual(0, sq.Set.Count);
        Assert.AreEqual("", sq.From_FullnameRegex);
        Assert.AreEqual("", sq.From_Attr);
        Assert.AreEqual("", sq.To_FullnameRegex);
        Assert.AreEqual("", sq.To_Attr);
        Assert.AreEqual("", sq.Where_FullnameRegex);
        Assert.AreEqual("([(Alpha Cee)(Beta)]{Eee})", sq.Where_Attr);
    }

    /// <summary>
    /// 構文解析 Transition Insert 文
    /// </summary>
    [Test]
    public void N70_ParseStatement_TransitionInsert()
    {
        string query = @"TRANSITION INSERT
                        SET Duration 0 ExitTime 1
                        FROM ""Base Layer.SMove""
                        TO ATTR (BusyX Block)";
        QueryTokens sq;
        bool successful = SyntaxP.ParseStatement_TransitionInsert(query, out sq);

        Assert.IsTrue(successful);
        Assert.AreEqual(QueryTokens.TRANSITION, sq.Target);
        Assert.AreEqual(QueryTokens.INSERT, sq.Manipulation);
        Assert.AreEqual(2, sq.Set.Count);
        Assert.IsTrue(sq.Set.ContainsKey("Duration"));
        Assert.AreEqual("0", sq.Set["Duration"]);
        Assert.IsTrue(sq.Set.ContainsKey("ExitTime"));
        Assert.AreEqual("1", sq.Set["ExitTime"]);
        Assert.AreEqual("Base Layer.SMove", sq.From_FullnameRegex);
        Assert.AreEqual("", sq.From_Attr);
        Assert.AreEqual("", sq.To_FullnameRegex);
        Assert.AreEqual("(BusyX Block)", sq.To_Attr);
    }

    /// <summary>
    /// 構文解析 Transition Update 文
    /// </summary>
    [Test]
    public void N70_ParseStatement_TransitionUpdate()
    {
        string query = @"TRANSITION UPDATE
                        SET Duration 0.25 ExitTime 0.75
                        FROM ""Base Layer.SMove""
                        TO ATTR (BusyX Block)";
        QueryTokens sq;
        bool successful = SyntaxP.ParseStatement_TransitionUpdate(query, out sq);

        Assert.IsTrue(successful);
        Assert.AreEqual(QueryTokens.TRANSITION, sq.Target);
        Assert.AreEqual(QueryTokens.UPDATE, sq.Manipulation);
        Assert.AreEqual(2, sq.Set.Count);
        Assert.IsTrue(sq.Set.ContainsKey("Duration"));
        Assert.AreEqual("0.25", sq.Set["Duration"]);
        Assert.IsTrue(sq.Set.ContainsKey("ExitTime"));
        Assert.AreEqual("0.75", sq.Set["ExitTime"]);
        Assert.AreEqual("Base Layer.SMove", sq.From_FullnameRegex);
        Assert.AreEqual("", sq.From_Attr);
        Assert.AreEqual("", sq.To_FullnameRegex);
        Assert.AreEqual("(BusyX Block)", sq.To_Attr);
    }

    /// <summary>
    /// 構文解析 Transition Delete 文
    /// </summary>
    [Test]
    public void N70_ParseStatement_TransitionDelete()
    {
        string query = @"TRANSITION DELETE
                        FROM ""Base Layer.SMove""
                        TO ATTR (BusyX Block)";
        QueryTokens sq;
        bool successful = SyntaxP.ParseStatement_TransitionDelete(query, out sq);

        Assert.IsTrue(successful);
        Assert.AreEqual(QueryTokens.TRANSITION, sq.Target);
        Assert.AreEqual(QueryTokens.DELETE, sq.Manipulation);
        Assert.AreEqual(0, sq.Set.Count);
        Assert.AreEqual("Base Layer.SMove", sq.From_FullnameRegex);
        Assert.AreEqual("", sq.From_Attr);
        Assert.AreEqual("", sq.To_FullnameRegex);
        Assert.AreEqual("(BusyX Block)", sq.To_Attr);
    }

    /// <summary>
    /// 構文解析 Transition Select 文
    /// </summary>
    [Test]
    public void N70_ParseStatement_TransitionSelect()
    {
        string query = @"TRANSITION SELECT
                        FROM ""Base Layer.SMove""
                        TO ATTR (BusyX Block)";
        QueryTokens sq;
        bool successful = SyntaxP.ParseStatement_TransitionSelect(query, out sq);

        Assert.IsTrue(successful);
        Assert.AreEqual(QueryTokens.TRANSITION, sq.Target);
        Assert.AreEqual(QueryTokens.SELECT, sq.Manipulation);
        Assert.AreEqual(0, sq.Set.Count);
        Assert.AreEqual("Base Layer.SMove", sq.From_FullnameRegex);
        Assert.AreEqual("", sq.From_Attr);
        Assert.AreEqual("", sq.To_FullnameRegex);
        Assert.AreEqual("(BusyX Block)", sq.To_Attr);
    }
    #endregion

    #region N80 lexical parser (字句パーサー)

    /// <summary>
    /// ATTR部を解析（１）
    /// </summary>
    [Test]
    public void N80_Parse_AttrParentesis_StringToTokens()
    {
        string attrParentesis = "([(Alpaca Bear)(Cat Dog)]{Elephant})";
        List<string> tokens;
        QueryTokens.String_to_tokens(attrParentesis, out tokens);

        //{ int i = 0; foreach (string token in tokens) { Debug.Log("Token[" + i + "]: " + token); i++; } }
        Assert.AreEqual(15, tokens.Count);
        Assert.AreEqual("(", tokens[0]);
        Assert.AreEqual("[", tokens[1]);
        Assert.AreEqual("(", tokens[2]);
        Assert.AreEqual("Alpaca", tokens[3]);
        Assert.AreEqual("Bear", tokens[4]);
        Assert.AreEqual(")", tokens[5]);
        Assert.AreEqual("(", tokens[6]);
        Assert.AreEqual("Cat", tokens[7]);
        Assert.AreEqual("Dog", tokens[8]);
        Assert.AreEqual(")", tokens[9]);
        Assert.AreEqual("]", tokens[10]);
        Assert.AreEqual("{", tokens[11]);
        Assert.AreEqual("Elephant", tokens[12]);
        Assert.AreEqual("}", tokens[13]);
        Assert.AreEqual(")", tokens[14]);
    }

    /// <summary>
    /// Parser 改行テスト
    /// </summary>
    [Test]
    public void N80_Parse_Newline()
    {
        int caret = 0;
        bool hit;
        string parenthesis;

        caret = 0;
        hit = LexcalP.VarSpaces(@"
a", ref caret);
        Assert.IsTrue(hit);
        Assert.AreEqual(2, caret); // 改行は 2 ？ 改行の文字数は環境依存か☆？

        caret = 0;
        hit = LexcalP.VarParentesis(@"(alpaca bear)
", ref caret, out parenthesis);
        Assert.IsTrue(hit);
        Assert.AreEqual(13 + 2, caret); // 改行は 2
        Assert.AreEqual("(alpaca bear)", parenthesis);
    }

    /// <summary>
    /// Parse関連のサブ関数。
    /// </summary>
    [Test]
    public void N80_Parse_SubFunctions()
    {
        int caret = 0;
        //StellaQLScanner.SkipSpace("  a",ref caret);
        //Assert.AreEqual(2, caret);

        bool hit;
        string word;
        string stringWithoutDoubleQuotation;
        string parenthesis;

        caret = 0;
        hit = LexcalP.VarSpaces("  a", ref caret);
        Assert.IsTrue(hit);
        Assert.AreEqual(2, caret);

        caret = 1;
        hit = LexcalP.VarSpaces("  a", ref caret);
        Assert.IsTrue(hit);
        Assert.AreEqual(2, caret);

        caret = 0;
        hit = LexcalP.VarSpaces("a  ", ref caret);
        Assert.IsFalse(hit);
        Assert.AreEqual(0, caret);

        caret = 0;
        hit = LexcalP.FixedWord("alpaca", "alpaca bear cat ", ref caret);
        Assert.IsTrue(hit);
        Assert.AreEqual(6 + 1, caret);
        hit = LexcalP.FixedWord("bear", "alpaca bear cat ", ref caret);
        Assert.IsTrue(hit, "caret=" + caret);
        Assert.AreEqual(11 + 1, caret);

        caret = 0;
        hit = LexcalP.FixedWord("alpaca", "alpaca  ", ref caret);
        Assert.IsTrue(hit);
        Assert.AreEqual(6 + 2, caret);

        caret = 0;
        hit = LexcalP.FixedWord("alpaca", "alpaca", ref caret);
        Assert.IsTrue(hit);
        Assert.AreEqual(6, caret);

        caret = 0;
        hit = LexcalP.FixedWord("alpaca", "alpxxx ", ref caret);
        Assert.IsFalse(hit);
        Assert.AreEqual(0, caret);

        caret = 0;
        hit = LexcalP.VarWord("alpaca ", ref caret, out word);
        Assert.IsTrue(hit);
        Assert.AreEqual(6 + 1, caret);
        Assert.AreEqual("alpaca", word);

        caret = 0;
        hit = LexcalP.VarWord(" alpaca", ref caret, out word);
        Assert.IsFalse(hit);
        Assert.AreEqual(0, caret);
        Assert.AreEqual("", word);

        caret = 0;
        hit = LexcalP.VarStringliteral(@"""alpaca"" ", ref caret, out stringWithoutDoubleQuotation);
        Assert.IsTrue(hit);
        Assert.AreEqual(8 + 1, caret);
        Assert.AreEqual("alpaca", stringWithoutDoubleQuotation);

        caret = 0;
        hit = LexcalP.VarStringliteral(@"""alpaca""", ref caret, out stringWithoutDoubleQuotation);
        Assert.IsTrue(hit);
        Assert.AreEqual(8, caret);
        Assert.AreEqual("alpaca", stringWithoutDoubleQuotation);

        caret = 0;
        hit = LexcalP.VarWord(@" alpaca ", ref caret, out stringWithoutDoubleQuotation);
        Assert.IsFalse(hit);
        Assert.AreEqual(0, caret);
        Assert.AreEqual("", stringWithoutDoubleQuotation);

        caret = 0;
        hit = LexcalP.VarParentesis(@"( alpaca ) ", ref caret, out parenthesis);
        Assert.IsTrue(hit);
        Assert.AreEqual(10 + 1, caret);
        Assert.AreEqual("( alpaca )", parenthesis);

        caret = 0;
        hit = LexcalP.VarWord(@"( alpaca ", ref caret, out parenthesis);
        Assert.IsFalse(hit);
        Assert.AreEqual(0, caret);
        Assert.AreEqual("", parenthesis);

        caret = 0;
        hit = LexcalP.VarParentesis(@"(alpaca) ", ref caret, out parenthesis);
        Assert.IsTrue(hit);
        Assert.AreEqual(8 + 1, caret);
        Assert.AreEqual("(alpaca)", parenthesis);

        caret = 0;
        hit = LexcalP.VarParentesis(@"(alpaca bear) ", ref caret, out parenthesis);
        Assert.IsTrue(hit);
        Assert.AreEqual(13 + 1, caret);
        Assert.AreEqual("(alpaca bear)", parenthesis);

        caret = 0;
        hit = LexcalP.VarParentesis(@"(alpaca bear)", ref caret, out parenthesis);
        Assert.IsTrue(hit);
        Assert.AreEqual(13, caret);
        Assert.AreEqual("(alpaca bear)", parenthesis);
    }
    #endregion
}
