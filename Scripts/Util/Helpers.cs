using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

namespace JLib.Utils
{
    public static class Helpers
    {
        public static void DestroyChildren(this Transform t)
        {
            foreach (Transform child in t)
            {
                Object.Destroy(child.gameObject);
            }
        }

        public static void ToggleMouse(KeyCode control) //use a keycode to toggle on or off.
        {
            if (!Input.GetKeyDown(control)) return;
            Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;

        }

        /// <summary>
        /// Return a rounded up vector.
        /// </summary>
        public static Vector3 VCeil(Vector3 vector) //round up all values
        {
            return new Vector3(
                Mathf.Ceil(vector.x),
                Mathf.Ceil(vector.y),
                Mathf.Ceil(vector.z)
                );
            
        }
        /// <summary>
        /// Return a vector that is rounded up if positive, and rounded down if negative.
        /// </summary>
        public static Vector3 VZeroRound(Vector3 vector) //round up if positive, round down if negative
        {
            return new Vector3(
                vector.x > 0 ? Mathf.Ceil(vector.x) : Mathf.Floor(vector.x), //if vector component greater than 0, ceiling it, else floor it
                vector.y > 0 ? Mathf.Ceil(vector.y) : Mathf.Floor(vector.y),
                vector.z > 0 ? Mathf.Ceil(vector.z) : Mathf.Floor(vector.z)
                );
        }
        public static GameObject InstantiateVisualEffect(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            // Instantiate the prefab
            GameObject obj = Object.Instantiate(prefab, position, rotation);

            // Get all the renderers of the object and its children
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();

            // Create new material instances for each renderer
            foreach (Renderer renderer in renderers)
            {
                Material[] materials = renderer.materials;
                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i] = new Material(materials[i]);
                }
                renderer.materials = materials;
            }

            return obj;
        }

        public static bool Roll(float percentChance)
        {
            if (percentChance <= 0f)
                return false;
            float roll = Random.Range(0f, 100f);
            if (roll <= percentChance)
            {
                //if the roll succeeds
                
                return true;
            }
            return false;
            
        }
        public static bool Roll(DiceType dice, int dc, int[] modifiers = null)
    {
        // Ensure modifiers is not null
        int sumModifiers = (modifiers != null) ? modifiers.Sum() : 0;

        // Validate the dice parameter
        if (dice <= 0)
        {
            throw new System.ArgumentException("Invalid dice value. It should be greater than 0.", nameof(dice));
        }
        // Generate the roll
        int roll = Random.Range(1, (int)dice) + sumModifiers;
        // Check for success
        return roll >= dc;
    }
        public enum DiceType
        {
            D2 = 2,
            D3 = 3,
            D4 = 4,
            D6 = 6,
            D8 = 8,
            D10 = 10,
            D12 = 12,
            D20 = 20,
            D100 = 100
        }
    /// <summary>
    /// Populates static fields of a specified type with assets from a given dictionary.
    /// </summary>
    /// <typeparam name="T">Type constraint for the assets; must derive from UnityEngine.Object.</typeparam>
    /// <param name="typeToPopulate">The type whose static fields need to be populated.</param>
    /// <param name="assets">A dictionary containing asset references with field names as keys.</param>
    public static void PopulateTypeFields<T>(System.Type typeToPopulate, Dictionary<string, T> assets) where T : UnityEngine.Object
    {
        // Iterate through static, public fields of the specified type
        foreach (FieldInfo field in typeToPopulate.GetFields(BindingFlags.Static | BindingFlags.Public))
        {
            // Check if the field type matches the specified asset type
            if (field.FieldType == typeof(T))
            {
                string fieldName = field.Name;

                // Check if the dictionary contains an asset with the field name
                if (assets.ContainsKey(fieldName))
                {
                    // Assign the asset to the static field
                    field.SetValue(null, assets[fieldName]);
                }
                else
                {
                    // Log a warning if the asset is not found in the dictionary
                    Debug.LogWarning($"Failed to assign {field.DeclaringType.Name}.{field.Name}: Asset \"{fieldName}\" not found.");
                }
            }
        }
    }

        public static float RandomRange(Vector2 vector)
        {
            return Random.Range(vector.x, vector.y);
        }

        

    }

}
