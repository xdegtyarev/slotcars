using UnityEngine;

public interface IUIContainerItem {

    void Setup(object itemData, IUIContainer container);
    void UpdateState();
    GameObject GetGameObject();

}

public interface IUITypedContainerItem: IUIContainerItem {
    System.Type GetDataType();
}

