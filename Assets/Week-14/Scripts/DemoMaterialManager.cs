using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Showcasing loading in materials using the Resources folder
 * Doesn't require a Material Manager in the scene to work as it's static
 */


namespace Week14
{
    public static class DemoMaterialManager
    {
        //Properties
        private static Material material1;
        private static Material material2;
        private static Material material3;
        private static bool initialized;

        //Methods
        private static void Initialize()
        {
            //Getting the path to the thing we want to load it from
            //Will return an object (Means anything there) (In this case, it's giving us a material)
            //Resources.Load("Materials/1");

            //This is the type casting version that will give us the material
            material1 = (Material)Resources.Load("Materials/1") as Material;
            material2 = (Material)Resources.Load("Materials/2") as Material;
            material3 = (Material)Resources.Load("Materials/3") as Material;

            initialized = true;
        }

        public static Material GetMaterial(int id)
        {
            if (initialized == false) { Initialize(); }


            switch (id)
            {
                case 1:
                    return material1;
                case 2:
                    return material2;
                case 3:
                    return material3;
                default:
                    return material1;
            }
        }
    }
}
