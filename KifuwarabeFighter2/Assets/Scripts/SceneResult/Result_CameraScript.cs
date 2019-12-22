﻿namespace SceneResult
{
    using Assets.Scripts.Model.Dto.Input;
    using Assets.Scripts.Model.Dto.Scene.Common;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class Result_CameraScript : MonoBehaviour
    {
        /// <summary>
        /// message. 勝利後メッセージ。
        /// </summary>
        Text text;
        /// <summary>
        /// player's face(win or lose). プレイヤーの顔。
        /// </summary>
        Image[] player_to_face;

        void Start()
        {
            text = GameObject.Find(SceneCommon.GAMEOBJ_TEXT).GetComponent<Text>();

            // プレイヤー１、２の顔
            {
                player_to_face = new Image[] { GameObject.Find(SceneCommon.PlayerAndGameobject_to_path[(int)PlayerIndex.Player1, (int)GameobjectIndex.Face]).GetComponent<Image>(), GameObject.Find(SceneCommon.PlayerAndGameobject_to_path[(int)PlayerIndex.Player2, (int)GameobjectIndex.Face]).GetComponent<Image>() };

                for (int iPlayer = (int)PlayerIndex.Player1; iPlayer < (int)PlayerIndex.Num; iPlayer++)
                {
                    int character = (int)CommonScript.Player_to_useCharacter[iPlayer];
                    Sprite[] sprites = Resources.LoadAll<Sprite>(CommonScript.CharacterAndSlice_to_faceSprites[character, (int)ResultFaceSpriteIndex.All]);
                    string slice;
                    switch (CommonScript.Result)
                    {
                        case Result.Player1_Win:
                            switch ((PlayerIndex)iPlayer)
                            {
                                case PlayerIndex.Player1: slice = CommonScript.CharacterAndSlice_to_faceSprites[character, (int)ResultFaceSpriteIndex.Win]; break;
                                case PlayerIndex.Player2: slice = CommonScript.CharacterAndSlice_to_faceSprites[character, (int)ResultFaceSpriteIndex.Lose]; break;
                                default: Debug.LogError("未定義のプレイヤー☆"); slice = ""; break;
                            }
                            break;
                        case Result.Player2_Win:
                            switch ((PlayerIndex)iPlayer)
                            {
                                case PlayerIndex.Player1: slice = CommonScript.CharacterAndSlice_to_faceSprites[character, (int)ResultFaceSpriteIndex.Lose]; break;
                                case PlayerIndex.Player2: slice = CommonScript.CharacterAndSlice_to_faceSprites[character, (int)ResultFaceSpriteIndex.Win]; break;
                                default: Debug.LogError("未定義のプレイヤー☆"); slice = ""; break;
                            }
                            break;
                        //case Result.Double_KnockOut:
                        //    break;
                        default:
                            // 開発中画面などで☆
                            slice = CommonScript.CharacterAndSlice_to_faceSprites[character, (int)ResultFaceSpriteIndex.Win];
                            break;
                    }
                    player_to_face[iPlayer].sprite = System.Array.Find<Sprite>(sprites, (sprite) => sprite.name.Equals(slice));
                }
            }

            switch (CommonScript.Result)
            {
                case Result.Player1_Win:
                    text.text = SceneCommon.Character_To_WinMessage[(int)CommonScript.Player_to_useCharacter[(int)PlayerIndex.Player1]];
                    break;
                case Result.Player2_Win:
                    text.text = SceneCommon.Character_To_WinMessage[(int)CommonScript.Player_to_useCharacter[(int)PlayerIndex.Player2]];
                    break;
                //case Result.Double_KnockOut:
                //    text.text = "ダブルＫＯ！\n";
                //    break;
                default:
                    text.text = "結果は\nどうなったんだぜ☆（＾～＾）？";
                    break;
            }
        }

        // Update is called once per frame
        void Update()
        {

            // 何かボタンを押したらセレクト画面へ遷移
            if (Input.GetButton(CommonInput.InputNameDictionary[InputIndexes.P1Lp]) ||
                Input.GetButton(CommonInput.InputNameDictionary[InputIndexes.P1Mp]) ||
                Input.GetButton(CommonInput.InputNameDictionary[InputIndexes.P1Hp]) ||
                Input.GetButton(CommonInput.InputNameDictionary[InputIndexes.P1Lk]) ||
                Input.GetButton(CommonInput.InputNameDictionary[InputIndexes.P1Mk]) ||
                Input.GetButton(CommonInput.InputNameDictionary[InputIndexes.P1Hk]) ||
                Input.GetButton(CommonInput.InputNameDictionary[InputIndexes.P1Pause]) ||
                Input.GetButton(CommonInput.Input10Ca) ||
                Input.GetButton(CommonInput.InputNameDictionary[InputIndexes.P2Lp]) ||
                Input.GetButton(CommonInput.InputNameDictionary[InputIndexes.P2Mp]) ||
                Input.GetButton(CommonInput.InputNameDictionary[InputIndexes.P2Hp]) ||
                Input.GetButton(CommonInput.InputNameDictionary[InputIndexes.P2Lk]) ||
                Input.GetButton(CommonInput.InputNameDictionary[InputIndexes.P2Mk]) ||
                Input.GetButton(CommonInput.InputNameDictionary[InputIndexes.P2Hp]) ||
                Input.GetButton(CommonInput.InputNameDictionary[InputIndexes.P2Pause])
            )
            {
                SceneManager.LoadScene(CommonScript.Scene_to_name[(int)SceneIndex.Select]);
            }

        }
    }
}
