using System.Collections.Generic;
using MyOwn.Model;
using UnityEditor;
using UnityEngine;

namespace Model.Editor
{
    [CustomEditor(typeof(ItemsDatabase))]
    public sealed class ItemsDatabaseEditor : UnityEditor.Editor
    {
        [SerializeField] private List<bool> elementFoldouts;

        private bool _foldout;
        
        private string _itemName;
        private string _itemID;
        private Sprite _itemSprite;
        private ItemType _itemType;
        private int _itemPrice;
        private WeaponType _weaponType;

        private SerializedProperty _itemsDatabase;

        private void OnEnable()
        {
            _itemsDatabase = serializedObject.FindProperty("items");
            elementFoldouts ??= new List<bool>(_itemsDatabase.arraySize);
            for (int i = elementFoldouts.Count; i < _itemsDatabase.arraySize; i++)
            {
                elementFoldouts.Add(default);
            }
        }

        public override void OnInspectorGUI()
        {
            var arraySize = _itemsDatabase.arraySize;
            
            EditorGUILayout.BeginHorizontal();
            _foldout = EditorGUILayout.Foldout(_foldout, "List");
            EditorGUILayout.IntField(arraySize, new GUILayoutOption[]
            {
                GUILayout.Width(30), 
            });
            EditorGUILayout.EndHorizontal();
            if (_foldout)
            {
                EditorGUI.indentLevel++;
                for (int i = 0; i < _itemsDatabase.arraySize; i++)
                {
                    var elementFoldout = EditorGUILayout.Foldout(elementFoldouts[i], $"Element {i}");
                    elementFoldouts[i] = elementFoldout;
                    if (elementFoldout)
                    {
                        EditorGUI.indentLevel++;
                        var index = i;
                        DrawItemData(_itemsDatabase.GetArrayElementAtIndex(i).managedReferenceValue as ItemData, index);
                        EditorGUI.indentLevel--;
                    }
                }
                EditorGUI.indentLevel--;
            }
            
            EditorGUILayout.Space(25);
            
            EditorGUILayout.LabelField("Add new item");
            EditorGUI.indentLevel++;
            _itemName = EditorGUILayout.TextField("Item name", _itemName);
            _itemID = EditorGUILayout.TextField("ID", _itemID);
            _itemSprite = EditorGUILayout.ObjectField("Icon", _itemSprite, typeof(Sprite), false) as Sprite;
            _itemType = (ItemType)EditorGUILayout.EnumPopup("Item Type", _itemType);
            if (_itemType == ItemType.Weapon)
                _weaponType = (WeaponType)EditorGUILayout.EnumPopup("Weapon Type", _weaponType);
            _itemPrice = EditorGUILayout.IntField("Item Price", _itemPrice);
            
            EditorGUI.indentLevel--;
            
            if (EditorGUILayout.LinkButton("Add"))
            {
                ItemData itemData;
                if (_itemType == ItemType.Weapon)
                {
                    var weaponData = new WeaponData();
                    weaponData.SetWeaponType(_weaponType);

                    itemData = weaponData;
                }
                else
                {
                    itemData = new ItemData();
                }

                itemData.SetName(_itemName)
                    .SetID(_itemID)
                    .SetSprite(_itemSprite)
                    .SetItemType(_itemType)
                    .SetPrice(_itemPrice);

                _itemsDatabase.arraySize++;
                _itemsDatabase.GetArrayElementAtIndex(_itemsDatabase.arraySize - 1).managedReferenceValue = itemData;
                elementFoldouts.Add(default);

                _itemName = string.Empty;
                _itemID = string.Empty;
                _itemSprite = null;
                _itemType = ItemType.None;
                _weaponType = WeaponType.None;
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawItemData(ItemData itemData, int index)
        {
            itemData.SetName(EditorGUILayout.TextField("Item name", itemData.Name))
                .SetID(EditorGUILayout.TextField("ID", itemData.ID))
                .SetSprite(EditorGUILayout.ObjectField("Icon", itemData.Sprite, typeof(Sprite), false) as Sprite)
                .SetItemType((ItemType)EditorGUILayout.EnumPopup("Item Type", itemData.ItemType))
                .SetPrice(EditorGUILayout.IntField("Item Price", itemData.Price));

            if (itemData is WeaponData weaponData)
            {
                weaponData.SetWeaponType((WeaponType)EditorGUILayout.EnumPopup("Weapon Type", weaponData.WeaponType));
            }
            
            if (EditorGUILayout.LinkButton("Delete"))
            {
                DeleteItem(index);
            }

            EditorGUILayout.Space(15);
        }

        private void DeleteItem(int index)
        {
            var length = _itemsDatabase.arraySize;
            for (int i = index; i < length - 1; i++)
            {
                elementFoldouts[i] = elementFoldouts[i + 1];
                _itemsDatabase.GetArrayElementAtIndex(i).managedReferenceValue =
                    _itemsDatabase.GetArrayElementAtIndex(i + 1).managedReferenceValue;
            }

            _itemsDatabase.arraySize--;
            elementFoldouts.RemoveAt(elementFoldouts.Count - 1);
        }
    }
}