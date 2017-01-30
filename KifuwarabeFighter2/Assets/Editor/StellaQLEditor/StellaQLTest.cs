﻿using NUnit.Framework;
using SceneStellaQLTest;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Text;

namespace StellaQL
{
    public class StellaQLTest
    {
        const string path_AnimationController = "Assets/Scripts/StellaQLEngine/Anicon@StellaQL.controller";

        #region N30 Execute Query (文字列を与えて、レコード・インデックスを取ってくる)
        [Test]
        public void N30_Query_StateSelect()
        {
            string query = @"STATE SELECT
                        WHERE TAG ([(Alpha Cee)(Beta)]{Eee})";
            HashSet<int> recordHashes;
            StringBuilder message = new StringBuilder();
            bool successful = Querier.ExecuteStateSelect(query, UserDefinedStateTable.Instance.StateHash_to_record, out recordHashes, message);

            Assert.IsTrue(successful);
            Assert.AreEqual(3, recordHashes.Count);
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ALPACA)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_CAT)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_RABBIT)));
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

                        WHERE TAG ([(Alpha Cee)(Beta)]{Eee})

                        # コメントＣ

                        ";
            // STATE SELECT文が動けば OK☆
            HashSet<int> recordHashes;
            StringBuilder message = new StringBuilder();
            bool successful = Querier.ExecuteStateSelect(query, UserDefinedStateTable.Instance.StateHash_to_record, out recordHashes, message);

            Assert.IsTrue(successful);
            Assert.AreEqual(3, recordHashes.Count);
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ALPACA)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_CAT)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_RABBIT)));
        }

        [Test]
        public void N30_Query_TransitionSelect()
        {
            string query = @"TRANSITION SELECT
                        FROM ""Base Layer\.Zebra""
                        TO TAG ([(Alpha Cee)(Beta)]{Eee})";
            HashSet<int> recordHashesSrc;
            HashSet<int> recordHashesDst;
            StringBuilder message = new StringBuilder();
            bool successful = Querier.ExecuteTransitionSelect(query, UserDefinedStateTable.Instance.StateHash_to_record, out recordHashesSrc, out recordHashesDst, message);

            Assert.AreEqual(1, recordHashesSrc.Count);
            Assert.IsTrue(recordHashesSrc.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ZEBRA)));
            Assert.AreEqual(3, recordHashesDst.Count);
            Assert.IsTrue(recordHashesDst.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ALPACA)));
            Assert.IsTrue(recordHashesDst.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_CAT)));
            Assert.IsTrue(recordHashesDst.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_RABBIT)));
        }

        [Test]
        public void N30_Query_TransitionAnysateInsert()
        {
            AnimatorController ac = AssetDatabase.LoadAssetAtPath<AnimatorController>(path_AnimationController);// アニメーター・コントローラーを取得。
            string query = @"TRANSITION ANYSTATE INSERT
                            FROM ""Base Layer""
                            TO ""Base Layer\.Foo""";
            StringBuilder message = new StringBuilder();
            bool successful = Querier.Execute(ac, query, UserDefinedStateTable.Instance, message);

            Debug.Log(message.ToString());
            Assert.IsTrue(successful);
        }
        [Test]
        public void N30_Query_TransitionEntryInsert()
        {
            AnimatorController ac = AssetDatabase.LoadAssetAtPath<AnimatorController>(path_AnimationController);// アニメーター・コントローラーを取得。
            string query = @"TRANSITION ENTRY INSERT
                            FROM ""Base Layer""
                            TO ""Base Layer\.Foo""";
            StringBuilder message = new StringBuilder();
            bool successful = Querier.Execute(ac, query, UserDefinedStateTable.Instance, message);

            Debug.Log(message.ToString());
            Assert.IsTrue(successful);
        }
        [Test]
        public void N30_Query_TransitionExitInsert()
        {
            AnimatorController ac = AssetDatabase.LoadAssetAtPath<AnimatorController>(path_AnimationController);// アニメーター・コントローラーを取得。
            string query = @"TRANSITION EXIT INSERT
                            FROM ""Base Layer\.Foo""";
            StringBuilder message = new StringBuilder();
            bool successful = Querier.Execute(ac, query, UserDefinedStateTable.Instance, message);

            Debug.Log(message.ToString());
            Assert.IsTrue(successful);
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
            RecordsFilter.TokenLockers_to_recordHashesLockers(
                tokenLockers, tokenLockersOperation, UserDefinedStateTable.Instance.StateHash_to_record, out recordHashesLockers);

            Assert.AreEqual(5, recordHashesLockers.Count);
            Assert.AreEqual(2, recordHashesLockers[0].Count);
            Assert.IsTrue(recordHashesLockers[0].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ALPACA)));
            Assert.IsTrue(recordHashesLockers[0].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_CAT)));
            Assert.AreEqual(3, recordHashesLockers[1].Count);
            Assert.IsTrue(recordHashesLockers[1].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_BEAR)));
            Assert.IsTrue(recordHashesLockers[1].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_RABBIT)));
            Assert.IsTrue(recordHashesLockers[1].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ZEBRA)));
            Assert.AreEqual(5, recordHashesLockers[2].Count);
            Assert.IsTrue(recordHashesLockers[2].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ALPACA)));
            Assert.IsTrue(recordHashesLockers[2].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_CAT)));
            Assert.IsTrue(recordHashesLockers[2].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_BEAR)));
            Assert.IsTrue(recordHashesLockers[2].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_RABBIT)));
            Assert.IsTrue(recordHashesLockers[2].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ZEBRA)));
            Assert.AreEqual(19, recordHashesLockers[3].Count);
            Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(UserDefinedStateTable.STATEMACHINE_BASELAYER)));
            Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ANYSTATE)));
            Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ENTRY)));
            Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_EXIT)));
            Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_FOO)));
            Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ALPACA)));
            Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_CAT)));
            Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_DOG)));
            Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_FOX)));
            Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_IGUANA)));
            Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_KANGAROO)));
            Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_LION)));
            Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_NUTRIA)));
            Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_OX)));
            Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_PIG)));
            Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_RABBIT)));
            Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_UNICORN)));
            Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_WOLF)));
            Assert.IsTrue(recordHashesLockers[3].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_YAK)));
            Assert.AreEqual(3, recordHashesLockers[4].Count);
            Assert.IsTrue(recordHashesLockers[4].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ALPACA)));
            Assert.IsTrue(recordHashesLockers[4].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_CAT)));
            Assert.IsTrue(recordHashesLockers[4].Contains(Animator.StringToHash(UserDefinedStateTable.STATE_RABBIT)));
        }
        #endregion

        #region N50 RecordsFilter (レコード集合)
        /// <summary>
        /// （　）要素フィルター
        /// </summary>
        [Test]
        public void N50_RecordsFilter_RecordsAnd()
        {
            // 条件は　「 Bear, Elephant 」 AND 「 Bear, Giraffe 」
            HashSet<int> lockerNumbers = new HashSet<int>() { 0, 1 };
            List<HashSet<int>> reordIndexLockers = new List<HashSet<int>>()
        {
            new HashSet<int>() { Animator.StringToHash(UserDefinedStateTable.STATE_BEAR),Animator.StringToHash(UserDefinedStateTable.STATE_ELEPHANT) },
            new HashSet<int>() { Animator.StringToHash(UserDefinedStateTable.STATE_BEAR), Animator.StringToHash(UserDefinedStateTable.STATE_GIRAFFE) },
        };
            HashSet<int> recordHashes = RecordsFilter.Records_And(lockerNumbers, reordIndexLockers);

            // 結果は　Bear
            Assert.AreEqual(1, recordHashes.Count);
            if (1 == recordHashes.Count)
            {
                Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_BEAR)));
            }
        }

        /// <summary>
        /// [ ] 要素フィルター
        /// </summary>
        [Test]
        public void N50_RecordsFilter_RecordsOr()
        {
            // 条件は　「 Bear, Elephant 」 OR 「 Bear, Giraffe 」
            HashSet<int> lockerNumbers = new HashSet<int>() { 0, 1 };
            List<HashSet<int>> recordHasheslockers = new List<HashSet<int>>()
        {
            new HashSet<int>() { Animator.StringToHash(UserDefinedStateTable.STATE_BEAR),Animator.StringToHash(UserDefinedStateTable.STATE_ELEPHANT) },
            new HashSet<int>() { Animator.StringToHash(UserDefinedStateTable.STATE_BEAR),Animator.StringToHash(UserDefinedStateTable.STATE_GIRAFFE) },
        };
            HashSet<int> recordHashes = RecordsFilter.Records_Or(lockerNumbers, recordHasheslockers);

            // 結果は　Bear, Elephant, Giraffe
            Assert.AreEqual(3, recordHashes.Count);
            if (3 == recordHashes.Count)
            {
                Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_BEAR)));
                Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ELEPHANT)));
                Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_GIRAFFE)));
            }
        }

        /// <summary>
        /// { } 要素フィルター
        /// </summary>
        [Test]
        public void N50_RecordsFilter_RecordsNotAndNot()
        {
            // 条件は　NOT「 Bear, Elephant 」 AND NOT「 Bear, Giraffe 」
            HashSet<int> lockerNumbers = new HashSet<int>() { 0, 1 };
            List<HashSet<int>> recordHashesLockers = new List<HashSet<int>>()
            {
                new HashSet<int>() { Animator.StringToHash(UserDefinedStateTable.STATE_BEAR),Animator.StringToHash(UserDefinedStateTable.STATE_ELEPHANT) },
                new HashSet<int>() { Animator.StringToHash(UserDefinedStateTable.STATE_BEAR),Animator.StringToHash(UserDefinedStateTable.STATE_GIRAFFE) },
            };
            HashSet<int> recordHashes = RecordsFilter.Records_NotAndNot(lockerNumbers, recordHashesLockers, UserDefinedStateTable.Instance.StateHash_to_record);

            // 結果は　"Base Layout","Any State","Entry","Exit",Foo,Alpaca,Cat,Dog,Fox,Horse,Iguana,Jellyfish,Kangaroo,Lion,Monkey,Nutria,Ox,Pig,Quetzal,Rabbit,Sheep,Tiger,Unicorn,Vixen,Wolf,Xenopus,Yak,Zebra
            Assert.AreEqual(28, recordHashes.Count);
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATEMACHINE_BASELAYER)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ANYSTATE)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ENTRY)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_EXIT)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_FOO)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ALPACA)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_CAT)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_DOG)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_FOX)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_HORSE)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_IGUANA)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_JELLYFISH)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_KANGAROO)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_LION)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_MONKEY)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_NUTRIA)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_OX)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_PIG)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_QUETZAL)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_RABBIT)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_SHEEP)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_TIGER)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_UNICORN)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_VIXEN)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_WOLF)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_XENOPUS)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_YAK)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ZEBRA)));
        }

        /// <summary>
        /// ステート名正規表現フィルター
        /// </summary>
        [Test]
        public void N50_RecordsFilter_StateFullNameRegex()
        {
            // 条件は、「Base Layer.」の下に、n または N が含まれるもの
            string pattern = @"Base Layer\.\w*[Nn]\w*";
            StringBuilder message = new StringBuilder();
            HashSet<int> recordHashes = RecordsFilter.String_StateFullNameRegex(pattern, UserDefinedStateTable.Instance.StateHash_to_record, message);

            // 結果は　"Any Stat", "Entry", Elephant、Iguana、Kangaroo、Lion、Monkey、Nutria、Unicorn、Vixen、Xenopus
            Assert.AreEqual(11, recordHashes.Count);
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ANYSTATE)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ENTRY)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ELEPHANT)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_IGUANA)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_KANGAROO)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_LION)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_MONKEY)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_NUTRIA)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_UNICORN)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_VIXEN)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_XENOPUS)));
        }

        /// <summary>
        /// （　）属性フィルター
        /// </summary>
        [Test]
        public void N50_RecordsFilter_TagsAnd()
        {
            // 条件は　Alpha | Eee
            HashSet<int> attrs = new HashSet<int>() { UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_ALPHA] , UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_EEE] };
            HashSet<int> recordHashes = RecordsFilter.Tags_And(attrs, UserDefinedStateTable.Instance.StateHash_to_record);

            // 結果は　Bear、Elephant、Giraffe、Quetzal、Zebra
            Assert.AreEqual(5, recordHashes.Count);
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_BEAR)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ELEPHANT)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_GIRAFFE)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_QUETZAL)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ZEBRA)));
        }

        /// <summary>
        /// [　] 属性フィルター
        /// </summary>
        [Test]
        public void N50_RecordsFilter_TagsOr()
        {
            // 条件は　（Alpha | Eee）、Beta、Eee
            HashSet<int> attrs = new HashSet<int>() {
                (int)(UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_ALPHA] |
                UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_EEE]) ,
                (int)UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_BETA],
                (int)UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_EEE] };
            HashSet<int> recordHashes = RecordsFilter.Tags_Or(attrs, UserDefinedStateTable.Instance.StateHash_to_record);

            // 結果は　Bear、Elephant、Giraffe、Horse、Jellyfish、Monkey、Quetzal、Rabbit、Sheep、Tiger、Vixen、Xenopus、Zebra
            //foreach (int hash in recordHashes) { Debug.Log("fullpath=[" + UserDefinedStateTable.Instance.StateHash_to_record[hash].Fullpath + "]"); }
            Assert.AreEqual(13, recordHashes.Count);
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_BEAR)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ELEPHANT)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_GIRAFFE)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_HORSE)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_JELLYFISH)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_MONKEY)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_QUETZAL)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_RABBIT)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_SHEEP)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_TIGER)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_VIXEN)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_XENOPUS)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ZEBRA)));
        }

        /// <summary>
        /// ｛　｝ 属性フィルター
        /// </summary>
        [Test]
        public void N50_RecordsFilter_TagsNotAndNot()
        {
            // 条件は　{ Beta、Eee }
            HashSet<int> attrs = new HashSet<int>() { UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_BETA], UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_EEE] };
            HashSet<int> recordHashes = RecordsFilter.Tags_NotAndNot(attrs, UserDefinedStateTable.Instance.StateHash_to_record);

            // 結果は　"Base Layout","Any State","Entry","Exit",Foo, Alpaca、Cat、Dog、Fox、Iguana、Kangaroo、Lion、Nutria、Ox、Pig、Unicorn、Wolf、Yak
            Assert.AreEqual(18, recordHashes.Count);
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATEMACHINE_BASELAYER)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ANYSTATE)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ENTRY)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_EXIT)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_FOO)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_ALPACA)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_CAT)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_DOG)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_FOX)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_IGUANA)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_KANGAROO)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_LION)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_NUTRIA)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_OX)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_PIG)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_UNICORN)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_WOLF)));
            Assert.IsTrue(recordHashes.Contains(Animator.StringToHash(UserDefinedStateTable.STATE_YAK)));
        }
        #endregion

        #region N60 Tag set ope (タグ集合)
        /// <summary>
        /// (　) で属性集合
        /// [　] で属性集合
        /// TODO: タグ検索に置き換え
        /// </summary>
        [Test]
        public void N60_ToAttrLocker_FromKeywordlistSet()
        {
            HashSet<int> attrLocker = new HashSet<int>() { UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_BETA], UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_DEE] };

            Assert.AreEqual(2, attrLocker.Count);
            Assert.IsTrue(attrLocker.Contains(UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_BETA]));
            Assert.IsTrue(attrLocker.Contains(UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_DEE]));
        }

        /// <summary>
        /// {　} で属性集合
        /// </summary>
        [Test]
        public void N60_ToAttrLocker_FromNGKeywordSet()
        {
            HashSet<int> set = new HashSet<int>() { UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_BETA], UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_DEE] };
            HashSet<int> attrLocker = TagSetOpe.Complement(set, new HashSet<int>(UserDefinedStateTable.Instance.TagString_to_hash.Values));

            Assert.AreEqual(5, attrLocker.Count);
            Assert.IsTrue(attrLocker.Contains(UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_ZERO]));
            Assert.IsTrue(attrLocker.Contains(UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_ALPHA]));
            Assert.IsTrue(attrLocker.Contains(UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_CEE]));
            Assert.IsTrue(attrLocker.Contains(UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_EEE]));
            Assert.IsTrue(attrLocker.Contains(UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_HORN]));
        }

        /// <summary>
        /// 補集合を取れるかテスト
        /// </summary>
        [Test]
        public void N60_ToAttrLocker_GetComplement()
        {
            // int型にして持つ
            HashSet<int> set = new HashSet<int>() { UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_BETA], UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_DEE] };
            HashSet<int> attrLocker = TagSetOpe.Complement(set, new HashSet<int>(UserDefinedStateTable.Instance.TagString_to_hash.Values));

            Assert.AreEqual(5, attrLocker.Count);
            Assert.IsTrue(attrLocker.Contains(UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_ZERO]));
            Assert.IsTrue(attrLocker.Contains(UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_ALPHA]));
            Assert.IsTrue(attrLocker.Contains(UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_CEE]));
            Assert.IsTrue(attrLocker.Contains(UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_EEE]));
            Assert.IsTrue(attrLocker.Contains(UserDefinedStateTable.Instance.TagString_to_hash[UserDefinedStateTable.TAG_HORN]));
        }
        #endregion

        #region N65 data builder (データ・ビルダー)
        /// <summary>
        /// TAG部を解析（２）
        /// </summary>
        [Test]
        public void N65_Parse_TagParentesis_TokensToLockers()
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
        public void N70_SyntaxPhrase_AfterSet()
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
        public void N70_SyntaxPhrase_AfterSet_Stringliteral()
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
        /// 構文解析 TRANSITION ANYSTATE INSERT 文
        /// </summary>
        [Test]
        public void N70_Syntax_TransitionAnystateInsert()
        {
            string query = @"TRANSITION ANYSTATE INSERT
                        FROM ""Base Layer""
                        TO ""Base Layer\.Foo""";
            QueryTokens sq;
            bool successful = SyntaxP.Parse_TransitionAnystateInsert(query, out sq);

            Assert.IsTrue(successful);
            Assert.AreEqual(QueryTokens.TRANSITION, sq.Target);
            Assert.AreEqual(QueryTokens.ANYSTATE, sq.Target2);
            Assert.AreEqual(QueryTokens.INSERT, sq.Manipulation);
            Assert.AreEqual(0, sq.Set.Count);
            Assert.AreEqual("Base Layer", sq.From_FullnameRegex);
            Assert.AreEqual("", sq.From_Attr);
            Assert.AreEqual(@"Base Layer\.Foo", sq.To_FullnameRegex);
            Assert.AreEqual("", sq.To_Tag);
            Assert.AreEqual("", sq.Where_FullnameRegex);
            Assert.AreEqual("", sq.Where_Tag);
        }

        /// <summary>
        /// 構文解析 TRANSITION ENTRY INSERT 文
        /// </summary>
        [Test]
        public void N70_Syntax_TransitionEntryInsert()
        {
            string query = @"TRANSITION ENTRY INSERT
                        FROM ""Base Layer""
                        TO ""Base Layer\.Foo""";
            QueryTokens sq;
            bool successful = SyntaxP.Parse_TransitionEntryInsert(query, out sq);

            Assert.IsTrue(successful);
            Assert.AreEqual(QueryTokens.TRANSITION, sq.Target);
            Assert.AreEqual(QueryTokens.ENTRY, sq.Target2);
            Assert.AreEqual(QueryTokens.INSERT, sq.Manipulation);
            Assert.AreEqual(0, sq.Set.Count);
            Assert.AreEqual("Base Layer", sq.From_FullnameRegex);
            Assert.AreEqual("", sq.From_Attr);
            Assert.AreEqual(@"Base Layer\.Foo", sq.To_FullnameRegex);
            Assert.AreEqual("", sq.To_Tag);
            Assert.AreEqual("", sq.Where_FullnameRegex);
            Assert.AreEqual("", sq.Where_Tag);
        }

        /// <summary>
        /// 構文解析 TRANSITION EXIT INSERT 文
        /// </summary>
        [Test]
        public void N70_Syntax_TransitionExitInsert()
        {
            string query = @"TRANSITION EXIT INSERT
                        FROM ""Base Layer\.Foo""";
            QueryTokens sq;
            bool successful = SyntaxP.Parse_TransitionExitInsert(query, out sq);

            Assert.IsTrue(successful);
            Assert.AreEqual(QueryTokens.TRANSITION, sq.Target);
            Assert.AreEqual(QueryTokens.EXIT, sq.Target2);
            Assert.AreEqual(QueryTokens.INSERT, sq.Manipulation);
            Assert.AreEqual(0, sq.Set.Count);
            Assert.AreEqual(@"Base Layer\.Foo", sq.From_FullnameRegex);
            Assert.AreEqual("", sq.From_Attr);
            Assert.AreEqual("", sq.To_FullnameRegex);
            Assert.AreEqual("", sq.To_Tag);
            Assert.AreEqual("", sq.Where_FullnameRegex);
            Assert.AreEqual("", sq.Where_Tag);
        }

        /// <summary>
        /// 構文解析 STATE INSERT 文
        /// </summary>
        [Test]
        public void N70_Syntax_StateInsert()
        {
            string query = @"STATE INSERT
                        SET name0 ""WhiteCat"" name1 ""WhiteDog""
                        WHERE ""Base Layout""";
            QueryTokens sq;
            bool successful = SyntaxP.Parse_StateInsert(query, out sq);

            Assert.IsTrue(successful);
            Assert.AreEqual(QueryTokens.STATE, sq.Target);
            Assert.AreEqual(QueryTokens.INSERT, sq.Manipulation);
            Assert.AreEqual(2, sq.Set.Count);
            Assert.AreEqual("WhiteCat", sq.Set["name0"]);
            Assert.AreEqual("WhiteDog", sq.Set["name1"]);
            Assert.AreEqual("", sq.From_FullnameRegex);
            Assert.AreEqual("", sq.From_Attr);
            Assert.AreEqual("", sq.To_FullnameRegex);
            Assert.AreEqual("", sq.To_Tag);
            Assert.AreEqual("Base Layout", sq.Where_FullnameRegex);
            Assert.AreEqual("", sq.Where_Tag);
        }

        /// <summary>
        /// 構文解析 STATE UPDATE 文
        /// </summary>
        [Test]
        public void N70_Syntax_StateUpdate()
        {
            string query = @"STATE UPDATE
                        SET name ""WhiteCat"" age 7
                        WHERE TAG ([(Alpha Cee)(Beta)]{Eee})";
            QueryTokens sq;
            bool successful = SyntaxP.Parse_StateUpdate(query, out sq);

            Assert.IsTrue(successful);
            Assert.AreEqual(QueryTokens.STATE, sq.Target);
            Assert.AreEqual(QueryTokens.UPDATE, sq.Manipulation);
            Assert.AreEqual(2, sq.Set.Count);
            Assert.AreEqual("WhiteCat", sq.Set["name"]);
            Assert.AreEqual("7", sq.Set["age"]);
            Assert.AreEqual("", sq.From_FullnameRegex);
            Assert.AreEqual("", sq.From_Attr);
            Assert.AreEqual("", sq.To_FullnameRegex);
            Assert.AreEqual("", sq.To_Tag);
            Assert.AreEqual("", sq.Where_FullnameRegex);
            Assert.AreEqual("([(Alpha Cee)(Beta)]{Eee})", sq.Where_Tag);
        }

        /// <summary>
        /// 構文解析 STATE DELETE 文
        /// </summary>
        [Test]
        public void N70_Syntax_StateDelete()
        {
            string query = @"STATE DELETE
                        SET name0 ""WhiteCat"" name1 ""WhiteDog""
                        WHERE ""Base Layout""";
            QueryTokens sq;
            bool successful = SyntaxP.Parse_StateDelete(query, out sq);

            Assert.IsTrue(successful);
            Assert.AreEqual(QueryTokens.STATE, sq.Target);
            Assert.AreEqual(QueryTokens.DELETE, sq.Manipulation);
            Assert.AreEqual(2, sq.Set.Count);
            Assert.AreEqual("WhiteCat", sq.Set["name0"]);
            Assert.AreEqual("WhiteDog", sq.Set["name1"]);
            Assert.AreEqual("", sq.From_FullnameRegex);
            Assert.AreEqual("", sq.From_Attr);
            Assert.AreEqual("", sq.To_FullnameRegex);
            Assert.AreEqual("", sq.To_Tag);
            Assert.AreEqual("Base Layout", sq.Where_FullnameRegex);
            Assert.AreEqual("", sq.Where_Tag);
        }

        /// <summary>
        /// 構文解析 STATE SELECT 文
        /// </summary>
        [Test]
        public void N70_Syntax_StateSelect()
        {
            string query = @"STATE SELECT
                        WHERE TAG ([(Alpha Cee)(Beta)]{Eee})";
            QueryTokens sq;
            bool successful = SyntaxP.Parse_StateSelect(query, out sq);

            Assert.IsTrue(successful);
            Assert.AreEqual(QueryTokens.STATE, sq.Target);
            Assert.AreEqual(QueryTokens.SELECT, sq.Manipulation);
            Assert.AreEqual(0, sq.Set.Count);
            Assert.AreEqual("", sq.From_FullnameRegex);
            Assert.AreEqual("", sq.From_Attr);
            Assert.AreEqual("", sq.To_FullnameRegex);
            Assert.AreEqual("", sq.To_Tag);
            Assert.AreEqual("", sq.Where_FullnameRegex);
            Assert.AreEqual("([(Alpha Cee)(Beta)]{Eee})", sq.Where_Tag);
        }

        /// <summary>
        /// 構文解析 Transition Insert 文
        /// </summary>
        [Test]
        public void N70_Syntax_TransitionInsert()
        {
            string query = @"TRANSITION INSERT
                        SET Duration 0 ExitTime 1
                        FROM ""Base Layer\.Cat""
                        TO ""Base Layer\.Dog""";
            QueryTokens sq;
            bool successful = SyntaxP.Parse_TransitionInsert(query, out sq);

            Assert.IsTrue(successful);
            Assert.AreEqual(QueryTokens.TRANSITION, sq.Target);
            Assert.AreEqual(QueryTokens.INSERT, sq.Manipulation);
            Assert.AreEqual(2, sq.Set.Count);
            Assert.IsTrue(sq.Set.ContainsKey("Duration"));
            Assert.AreEqual("0", sq.Set["Duration"]);
            Assert.IsTrue(sq.Set.ContainsKey("ExitTime"));
            Assert.AreEqual("1", sq.Set["ExitTime"]);
            Assert.AreEqual(@"Base Layer\.Cat", sq.From_FullnameRegex);
            Assert.AreEqual("", sq.From_Attr);
            Assert.AreEqual(@"Base Layer\.Dog", sq.To_FullnameRegex);
            Assert.AreEqual("", sq.To_Tag);
        }

        /// <summary>
        /// 構文解析 Transition Update 文
        /// </summary>
        [Test]
        public void N70_Syntax_TransitionUpdate()
        {
            // まず、線を引く。
            {
                string query = @"TRANSITION INSERT
                        SET Duration 0 ExitTime 1
                        FROM ""Base Layer\.Cat""
                        TO ""Base Layer\.Dog""";
                QueryTokens sq;
                bool successful = SyntaxP.Parse_TransitionInsert(query, out sq);
                Assert.IsTrue(successful);
            }

            // こっから本番
            {
                string query = @"TRANSITION UPDATE
                        SET Duration 0.25 ExitTime 0.75
                        FROM ""Base Layer\.Cat""
                        TO ""Base Layer\.Dog""";
                QueryTokens sq;
                bool successful = SyntaxP.Parse_TransitionUpdate(query, out sq);

                Assert.IsTrue(successful);
                Assert.AreEqual(QueryTokens.TRANSITION, sq.Target);
                Assert.AreEqual(QueryTokens.UPDATE, sq.Manipulation);
                Assert.AreEqual(2, sq.Set.Count);
                Assert.IsTrue(sq.Set.ContainsKey("Duration"));
                Assert.AreEqual("0.25", sq.Set["Duration"]);
                Assert.IsTrue(sq.Set.ContainsKey("ExitTime"));
                Assert.AreEqual("0.75", sq.Set["ExitTime"]);
                Assert.AreEqual(@"Base Layer\.Cat", sq.From_FullnameRegex);
                Assert.AreEqual("", sq.From_Attr);
                Assert.AreEqual(@"Base Layer\.Dog", sq.To_FullnameRegex);
                Assert.AreEqual("", sq.To_Tag);
            }
        }

        /// <summary>
        /// 構文解析 Transition Delete 文
        /// </summary>
        [Test]
        public void N70_Syntax_TransitionDelete()
        {
            string query = @"TRANSITION DELETE
                        FROM ""Base Layer.SMove""
                        TO TAG (BusyX Block)";
            QueryTokens sq;
            bool successful = SyntaxP.Parse_TransitionDelete(query, out sq);

            Assert.IsTrue(successful);
            Assert.AreEqual(QueryTokens.TRANSITION, sq.Target);
            Assert.AreEqual(QueryTokens.DELETE, sq.Manipulation);
            Assert.AreEqual(0, sq.Set.Count);
            Assert.AreEqual("Base Layer.SMove", sq.From_FullnameRegex);
            Assert.AreEqual("", sq.From_Attr);
            Assert.AreEqual("", sq.To_FullnameRegex);
            Assert.AreEqual("(BusyX Block)", sq.To_Tag);
        }

        /// <summary>
        /// 構文解析 Transition Select 文
        /// </summary>
        [Test]
        public void N70_Syntax_TransitionSelect()
        {
            string query = @"TRANSITION SELECT
                        FROM ""Base Layer.SMove""
                        TO TAG (BusyX Block)";
            QueryTokens sq;
            bool successful = SyntaxP.Parse_TransitionSelect(query, out sq);

            Assert.IsTrue(successful);
            Assert.AreEqual(QueryTokens.TRANSITION, sq.Target);
            Assert.AreEqual(QueryTokens.SELECT, sq.Manipulation);
            Assert.AreEqual(0, sq.Set.Count);
            Assert.AreEqual("Base Layer.SMove", sq.From_FullnameRegex);
            Assert.AreEqual("", sq.From_Attr);
            Assert.AreEqual("", sq.To_FullnameRegex);
            Assert.AreEqual("(BusyX Block)", sq.To_Tag);
        }
        #endregion

        #region N80 lexical parser (字句パーサー)
        /// <summary>
        /// TAG部を解析（１）
        /// </summary>
        [Test]
        public void N80_Lexical_TagParentesis_StringToTokens()
        {
            string attrParentesis = "([(Alpaca Bear)(Cat Dog)]{Elephant})";
            List<string> tokens;
            SyntaxPOther.String_to_tokens(attrParentesis, out tokens);

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
        public void N80_Lexical_Newline()
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

        [Test]
        public void N80_Lexical_VarStringliteral()
        {
            int caret = 0;
            const int DOUBLE_QUOT = 2;
            bool hit;
            string stringWithoutDoubleQuotation;

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
            hit = LexcalP.VarStringliteral(@"""alpaca.bear""", ref caret, out stringWithoutDoubleQuotation);
            Assert.IsTrue(hit);
            Assert.AreEqual(11+ DOUBLE_QUOT, caret);
            Assert.AreEqual("alpaca.bear", stringWithoutDoubleQuotation);

            caret = 0;
            hit = LexcalP.VarStringliteral(@"""alpaca\.bear""", ref caret, out stringWithoutDoubleQuotation);
            Assert.IsTrue(hit);
            Assert.AreEqual(12+ DOUBLE_QUOT, caret);
            Assert.AreEqual(@"alpaca\.bear", stringWithoutDoubleQuotation);

            caret = 0;
            hit = LexcalP.VarStringliteral(@"""Base Layer\.Any State""", ref caret, out stringWithoutDoubleQuotation);
            Assert.IsTrue(hit);
            Assert.AreEqual(21 + DOUBLE_QUOT, caret);
            Assert.AreEqual(@"Base Layer\.Any State", stringWithoutDoubleQuotation);
        }

        /// <summary>
        /// Parse関連のサブ関数。
        /// </summary>
        [Test]
        public void N80_Lexical_SubFunctions()
        {
            int caret = 0;
            bool hit;
            string word;
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
            hit = LexcalP.VarWord(@" alpaca ", ref caret, out word);
            Assert.IsFalse(hit);
            Assert.AreEqual(0, caret);
            Assert.AreEqual("", word);

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
}