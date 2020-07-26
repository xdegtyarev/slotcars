using UnityEngine;

public interface IUIContainer {
    event System.Action<object> OnItemClickAction;
    event System.Action<object> OnItemDoubleClickAction;
    void MarkItemClicked(object containerItemData);
    void ItemDoubleClicked(object containerItemData);
    IUIContainerItem CreateItem(object containerItemData, GameObject itemPrefab, bool setFirstSibling = false);
    void AddItem(IUIContainerItem containerItem, bool setFirstSibling = false);
    void RemoveItem(IUIContainerItem containerItem);
    void Clear();
}
