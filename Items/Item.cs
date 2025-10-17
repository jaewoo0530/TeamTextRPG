using TeamTextRPG.Entities;

namespace TeamTextRPG.Items
{
    public enum ItemType { 방어구, 무기, 소모템 }
    internal class Item
    {
        public ItemType ItemType { get; set; }

        public string name;

        public int value;
        public string info;
        public bool isEquip;
        public bool isHave;

        //장착템
        public Item(string name, ItemType itemType, int value, string info, bool isEquip, bool isHave)
        {
            this.name = name;
            ItemType = itemType;
            this.value = value;
            this.info = info;
            this.isEquip = isEquip;
            this.isHave = isHave;
        }

        //소모템
        public Item(string name, ItemType itemType, int value, string info)
        {
            this.name = name;
            ItemType = itemType;
            this.value = value;
            this.info = info;
        }

        public string TypeName => ItemType switch
        {
            ItemType.무기 => "공격력",
            ItemType.방어구 => "방어력",
            _ => "체력"
        };

        public virtual void Use(Character player)
        { }
    }
}
