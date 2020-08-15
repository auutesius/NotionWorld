using System;
using System.Data;
using System.Collections.Generic;
using NotionWorld.Capabilities;
using UnityEditor;
using UnityEngine;
using NotionWorld.IO;

namespace NotionWorld.Entities
{
    [CreateAssetMenu(fileName = "EntityArchetype", menuName = "NotionWorld/EntityArchetype", order = 0)]
    public sealed class EntityArchetype : ScriptableObject
    {
        public string id;

        public TextAsset csvFile;

        private static DataTable anchetypeTable;

        private delegate Capability CreateCapability(DataRow row, ref int index);

        private static readonly Dictionary<string, CreateCapability> creators = new Dictionary<string, CreateCapability>()
        {
            ["Health"] = (DataRow row, ref int index) =>
            {
                if (row[index] as string == string.Empty)
                {
                    return null;
                }
                return new Health() 
                { 
                    Value = int.Parse(row[index] as string),
                    MaxValue = int.Parse(row[++index] as string)
                };
            },
            ["Speed"] = (DataRow row, ref int index) =>
            {
                if (row[index] as string == string.Empty)
                {
                    return null;
                }
                return new Speed() { Value = float.Parse(row[index] as string) };
            },
            ["Attack"] = (DataRow row, ref int index) =>
            {
                if (row[index] as string == string.Empty)
                {
                    index += 4;
                    return null;
                }
                return new Attack()
                {
                    Value = int.Parse(row[index] as string),
                    Interval = float.Parse(row[++index] as string),
                    Range = float.Parse(row[++index] as string),
                    AttackType = (row[++index] as string),
                    BulletSpeed = float.Parse(row[++index] as string)
                };
            },
            ["Energy"] = (DataRow row, ref int index) =>
            {
                if (row[index] as string == string.Empty)
                {
                    index += 2;
                    return null;
                }
                return new Energy()
                {
                    Value = float.Parse(row[index] as string),
                    MaxValue = float.Parse(row[++index] as string),
                    Recharge = float.Parse(row[++index] as string)
                };
            },
            ["Skill"] = (DataRow row, ref int index) =>
            {
                if (row[index] as string == string.Empty)
                {
                    index++;
                    return null;
                }
                return new Skill()
                {
                    SkillTypes = (row[index] as string).Split(' ')
                };
            },
            ["BuffList"] = (DataRow row, ref int index) => {return new BuffList() ;}
        };

        internal Capability[] CreateCapabilities()
        {
            if (anchetypeTable == null)
            {
                anchetypeTable = DataTableCreator.Create(csvFile, "ArchetypeTable");
            }
            List<Capability> list = new List<Capability>(4);

            DataRow row = anchetypeTable.Select("ID = '" + id + "'")[0];
            int columnCount = anchetypeTable.Columns.Count;

            for (int i = 1; i < columnCount; i++)
            {
                string columnName = anchetypeTable.Columns[i].ColumnName;
                var capability = creators[columnName](row, ref i);
                if (capability != null)
                {
                    list.Add(capability);
                }
            }
            return list.ToArray();
        }
    }
}