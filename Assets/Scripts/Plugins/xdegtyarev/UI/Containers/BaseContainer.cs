using UnityEngine;
using System.Collections.Generic;


	public class BaseContainer : MonoBehaviour, IUIContainer {
	    public event System.Action<object> OnItemClickAction;
	    public event System.Action<object> OnItemDoubleClickAction;

		public List<IUIContainerItem> items = new List<IUIContainerItem>();

		public virtual IUIContainerItem CreateItem( object itemData, GameObject itemPrefab, bool setFirstSibling = false ) {
			GameObject itemGameObject = GameObject.Instantiate( itemPrefab ) as GameObject;
			IUIContainerItem item = itemGameObject.GetComponent( typeof( IUIContainerItem ) ) as IUIContainerItem;
			item.Setup( itemData, this );
			AddItem( item, setFirstSibling);
			return item;
		}

		public virtual void AddItem( IUIContainerItem item, bool setFirstSibling = false) {
			items.Add( item );
			item.GetGameObject().transform.SetParent( transform, false );
			if(setFirstSibling){
				item.GetGameObject().transform.SetAsFirstSibling();
			}
		}

		public virtual void RemoveItem( IUIContainerItem item ) {
			items.Remove( item );
			Destroy( item.GetGameObject() );
		}

		public virtual void Clear() {
				while ( items.Count != 0 ) {
					RemoveItem( items[0] );
				}
		}

		public virtual GameObject GetGameObject() {
			return gameObject;
		}

		public virtual void MarkItemClicked(object obj){
			if(OnItemClickAction!=null){
				OnItemClickAction(obj);
			}
		}

		public void ItemDoubleClicked(object obj) {
    		if(OnItemDoubleClickAction!=null){
    			OnItemDoubleClickAction(obj);
    		}
    	}

	}
