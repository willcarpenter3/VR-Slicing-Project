using UnityEngine;
using EzySlice;

/**
 * Source: https://github.com/LandVr/SliceMeshes
 */
public class Slicer : MonoBehaviour
{
    public Material materialAfterSlice;
    public LayerMask sliceMask;
    public bool isTouched;

    private void Update()
    {
        if (isTouched == true)
        {
            isTouched = false;

            //Magic numbers...
            Collider[] objectsToBeSliced = Physics.OverlapBox(transform.position, new Vector3(1, 0.1f, 0.1f), transform.rotation, sliceMask);

            foreach (Collider objectToBeSliced in objectsToBeSliced)
            {
                //Check to see if item should be dropped
                DropItem d = objectToBeSliced.gameObject.GetComponent<DropItem>();
                if (d != null)
                {
                    d.Drop();
                }

                SlicedHull slicedObject = SliceObject(objectToBeSliced.gameObject, materialAfterSlice);

                GameObject upperHullGameobject = slicedObject.CreateUpperHull(objectToBeSliced.gameObject, materialAfterSlice);
                GameObject lowerHullGameobject = slicedObject.CreateLowerHull(objectToBeSliced.gameObject, materialAfterSlice);

                //blood pool spawning code
                if (objectToBeSliced.CompareTag("PoolSpawner"))
                {
                    //Give upper & lower Hull objects the PoolSpawn script (if it's a pool spawner)
                    upperHullGameobject.AddComponent<PoolSpawn>();
                    lowerHullGameobject.AddComponent<PoolSpawn>();
                    GameObject p = objectToBeSliced.gameObject.GetComponent<PoolSpawn>().getPool();
                    if (p != null) // if a pool already exists, increase scale of existing pool
                    {
                        objectToBeSliced.GetComponent<PoolSpawn>().scale();
                    }
                    else // otherwise, spawn a new pool
                    {
                        p = objectToBeSliced.GetComponent<PoolSpawn>().spawn(objectToBeSliced.transform);
                    }
                    // pass pool onto "children"
                    upperHullGameobject.GetComponent<PoolSpawn>().setPool(p);
                    lowerHullGameobject.GetComponent<PoolSpawn>().setPool(p);
                }


                upperHullGameobject.transform.position = objectToBeSliced.transform.position;
                lowerHullGameobject.transform.position = objectToBeSliced.transform.position;

                MakeItPhysical(upperHullGameobject);
                MakeItPhysical(lowerHullGameobject);

                Destroy(objectToBeSliced.gameObject);
            }
        }
    }

    private void MakeItPhysical(GameObject obj)
    {
        obj.AddComponent<MeshCollider>().convex = true;
        obj.AddComponent<Rigidbody>();
        obj.layer = LayerMask.NameToLayer("Sliceable"); //Enables us to slice the same object multiple times :)
    }

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }
}
