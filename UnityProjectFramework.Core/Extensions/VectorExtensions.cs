
using UnityEngine;

namespace UnityProjectFramework.Core
{
    /// <summary>
    /// Provides extension methods for manipulating Unity's Vector2 and Vector3 structures.
    /// </summary>
    public static class VectorExtensions
    {
        #region Vector3

        /// <summary>
        /// Converts a Vector3 to a Vector2, discarding the z-component and retaining the x and y components.
        /// </summary>
        /// <param name="vector3">The Vector3 to be converted.</param>
        /// <returns>A Vector2 with the x and y components of the input Vector3.</returns>
        public static Vector2 ToXY(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.y);
        }

        /// <summary>
        /// Converts a Vector3 to a Vector2, discarding the y-component and retaining the x and z components.
        /// </summary>
        /// <param name="vector3">The Vector3 to be converted.</param>
        /// <returns>A Vector2 with the x and z components of the input Vector3.</returns>
        public static Vector2 ToXZ(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.z);
        }

        /// <summary>
        /// Sets the x-component of a Vector3 to the specified value and returns the modified Vector3.
        /// </summary>
        /// <param name="vector3">The Vector3 whose x-component is to be set.</param>
        /// <param name="x">The new value for the x-component.</param>
        /// <returns>The modified Vector3 with the updated x-component.</returns>
        public static Vector3 SetX(this Vector3 vector3, float x)
        {
            vector3.x = x;
            return vector3;
        }

        /// <summary>
        /// Updates the y-component of the specified Vector3 with the provided value.
        /// </summary>
        /// <param name="vector3">The Vector3 to be modified.</param>
        /// <param name="y">The new y-component value to set.</param>
        /// <returns>A new Vector3 with the updated y-component.</returns>
        public static Vector3 SetY(this Vector3 vector3, float y)
        {
            vector3.y = y;
            return vector3;
        }

        /// <summary>
        /// Sets the z-component of a Vector3 to the specified value and returns the modified Vector3.
        /// </summary>
        /// <param name="vector3">The Vector3 whose z-component will be set.</param>
        /// <param name="z">The new value for the z-component.</param>
        /// <returns>A Vector3 with its z-component updated to the specified value.</returns>
        public static Vector3 SetZ(this Vector3 vector3, float z)
        {
            vector3.z = z;
            return vector3;
        }

        /// <summary>
        /// Adds a specified value to the x-component of a Vector3 and returns the modified vector.
        /// </summary>
        /// <param name="vector3">The original Vector3 to be modified.</param>
        /// <param name="x">The value to be added to the x-component of the vector.</param>
        /// <returns>A new Vector3 with the updated x-component.</returns>
        public static Vector3 AddX(this Vector3 vector3, float x)
        {
            vector3.x += x;
            return vector3;
        }

        /// <summary>
        /// Adds a specified value to the y-component of the Vector3.
        /// </summary>
        /// <param name="vector3">The Vector3 to modify.</param>
        /// <param name="y">The value to add to the y-component.</param>
        /// <returns>The modified Vector3 with the updated y-component.</returns>
        public static Vector3 AddY(this Vector3 vector3, float y)
        {
            vector3.y += y;
            return vector3;
        }

        /// <summary>
        /// Adds the specified value to the z-component of the given Vector3 and returns the modified Vector3.
        /// </summary>
        /// <param name="vector3">The original Vector3 to which the z-component will be added.</param>
        /// <param name="z">The value to add to the z-component of the Vector3.</param>
        /// <returns>A new Vector3 with the updated z-component.</returns>
        public static Vector3 AddZ(this Vector3 vector3, float z)
        {
            vector3.z += z;
            return vector3;
        }

        /// <summary>
        /// Adds an offset value to the x, y, and z components of the given Vector3.
        /// </summary>
        /// <param name="vector3">The Vector3 to which the offset will be applied.</param>
        /// <param name="offset">The value to add to each component of the Vector3.</param>
        /// <returns>A new Vector3 with the offset added to its x, y, and z components.</returns>
        public static Vector3 AddOffset(this Vector3 vector3, float offset)
        {
            vector3.x += offset;
            vector3.y += offset;
            vector3.z += offset;
            return vector3;
        }

        /// <summary>
        /// Subtracts a specified value from the x-component of the Vector3 and returns the updated Vector3.
        /// </summary>
        /// <param name="vector3">The Vector3 instance to modify.</param>
        /// <param name="x">The value to subtract from the x-component.</param>
        /// <returns>A new Vector3 with the x-component adjusted by the specified value.</returns>
        public static Vector3 SubX(this Vector3 vector3, float x)
        {
            vector3.x -= x;
            return vector3;
        }

        /// <summary>
        /// Decreases the y-component of the given Vector3 by a specified value.
        /// </summary>
        /// <param name="vector3">The Vector3 whose y-component will be decreased.</param>
        /// <param name="y">The value to subtract from the y-component of the Vector3.</param>
        /// <returns>A Vector3 with the updated y-component.</returns>
        public static Vector3 SubY(this Vector3 vector3, float y)
        {
            vector3.y -= y;
            return vector3;
        }

        /// <summary>
        /// Subtracts a specified value from the z-component of a Vector3 and returns the updated Vector3.
        /// </summary>
        /// <param name="vector3">The original Vector3 whose z-component will be modified.</param>
        /// <param name="z">The value to subtract from the z-component.</param>
        /// <returns>A Vector3 with the updated z-component.</returns>
        public static Vector3 SubZ(this Vector3 vector3, float z)
        {
            vector3.z -= z;
            return vector3;
        }

        /// <summary>
        /// Subtracts a specified offset value from the x, y, and z components of a Vector3.
        /// </summary>
        /// <param name="vector3">The Vector3 to be modified.</param>
        /// <param name="offset">The offset value to subtract from each component.</param>
        /// <returns>A new Vector3 with the offset subtracted from each component.</returns>
        public static Vector3 SubOffset(this Vector3 vector3, float offset)
        {
            vector3.x -= offset;
            vector3.y -= offset;
            vector3.z -= offset;
            return vector3;
        }

        /// <summary>
        /// Adjusts a Vector3 by setting its y-component to zero, while keeping the x and z components unchanged.
        /// </summary>
        /// <param name="vector3">The Vector3 to be modified.</param>
        /// <returns>A Vector3 with the y-component set to zero and the x and z components unchanged.</returns>
        public static Vector3 FlattenY(this Vector3 vector3)
        {
            return new Vector3(vector3.x, 0f, vector3.z);
        }

        /// <summary>
        /// Projects a Vector3 onto a given normal vector.
        /// </summary>
        /// <param name="vector3">The Vector3 to be projected.</param>
        /// <param name="normal">The Vector3 normal onto which the projection is performed.</param>
        /// <returns>The projected Vector3 result.</returns>
        public static Vector3 Projection(this Vector3 vector3, Vector3 normal)
        {
            return Vector3.Project(vector3, normal);
        }

        #endregion

        #region Vector2

        /// <summary>
        /// Converts a Vector2 to a Vector3, assigning the z-component to the specified value, while retaining the x and y components of the Vector2.
        /// </summary>
        /// <param name="vector2">The Vector2 to be converted.</param>
        /// <param name="z">The value to assign to the z-component of the resulting Vector3.</param>
        /// <returns>A Vector3 with the x and y components from the Vector2 and the specified z value.</returns>
        public static Vector3 ToXY(this Vector2 vector2, float z = 0f)
        {
            return new Vector3(vector2.x, vector2.y, z);
        }

        /// <summary>
        /// Converts a Vector2 to a Vector3, discarding the y-component and retaining the x and z components.
        /// </summary>
        /// <param name="vector2">The Vector2 to be converted.</param>
        /// /// <param name="y">The value to assign to the y-component of the resulting Vector3.</param>
        /// <returns>A Vector3 with the x and y components from the Vector2 and the specified y value.</returns>
        public static Vector3 ToXZ(this Vector2 vector2, float y = 0f)
        {
            return new Vector3(vector2.x, y, vector2.y);
        }

        /// <summary>
        /// Sets the x-component of a Vector2 to the specified value, returning the modified Vector2.
        /// </summary>
        /// <param name="vector2">The original Vector2 to be modified.</param>
        /// <param name="x">The new value for the x-component.</param>
        /// <returns>A Vector2 with the updated x-component.</returns>
        public static Vector2 SetX(this Vector2 vector2, float x)
        {
            vector2.x = x;
            return vector2;
        }

        /// <summary>
        /// Sets the y-component of the specified Vector2 to the given value.
        /// </summary>
        /// <param name="vector2">The Vector2 whose y-component is to be set.</param>
        /// <param name="y">The new value for the y-component.</param>
        /// <returns>A Vector2 with the updated y-component.</returns>
        public static Vector2 SetY(this Vector2 vector2, float y)
        {
            vector2.y = y;
            return vector2;
        }

        /// <summary>
        /// Adds a specified value to the x-component of a Vector2.
        /// </summary>
        /// <param name="vector2">The Vector2 whose x-component will be modified.</param>
        /// <param name="x">The value to add to the x-component of the Vector2.</param>
        /// <returns>A Vector2 with the modified x-component.</returns>
        public static Vector2 AddX(this Vector2 vector2, float x)
        {
            vector2.x += x;
            return vector2;
        }

        /// <summary>
        /// Adds a specified value to the y-component of the given Vector2.
        /// </summary>
        /// <param name="vector2">The Vector2 instance whose y-component will be modified.</param>
        /// <param name="y">The value to add to the y-component.</param>
        /// <returns>A Vector2 with the updated y-component.</returns>
        public static Vector2 AddY(this Vector2 vector2, float y)
        {
            vector2.y += y;
            return vector2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vector2"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static Vector2 AddOffset(this Vector2 vector2, float offset)
        {
            vector2.x += offset;
            vector2.y += offset;
            return vector2;
        }

        /// <summary>
        /// Subtracts a specified value from the x-component of the given Vector2 and returns the updated vector.
        /// </summary>
        /// <param name="vector2">The Vector2 whose x-component will be modified.</param>
        /// <param name="x">The value to subtract from the x-component.</param>
        /// <returns>The updated Vector2 with the modified x-component.</returns>
        public static Vector2 SubX(this Vector2 vector2, float x)
        {
            vector2.x -= x;
            return vector2;
        }

        /// <summary>
        /// Subtracts a specified value from the y-component of a Vector2 and returns the updated vector.
        /// </summary>
        /// <param name="vector2">The Vector2 from which the value is to be subtracted.</param>
        /// <param name="y">The value to subtract from the y-component of the vector.</param>
        /// <returns>The updated Vector2 with the y-component reduced by the specified value.</returns>
        public static Vector2 SubY(this Vector2 vector2, float y)
        {
            vector2.y -= y;
            return vector2;
        }

        /// <summary>
        /// Subtracts a specified offset value from both the x and y components of a Vector2.
        /// </summary>
        /// <param name="vector2">The Vector2 from which the offset will be subtracted.</param>
        /// <param name="offset">The value to subtract from both x and y components.</param>
        /// <returns>A new Vector2 with the offset subtracted from its x and y components.</returns>
        public static Vector2 SubOffset(this Vector2 vector2, float offset)
        {
            vector2.x -= offset;
            vector2.y -= offset;
            return vector2;
        }

        #endregion
        
    }
}