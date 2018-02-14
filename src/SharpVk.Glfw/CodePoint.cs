using System.Text;

namespace SharpVk.Glfw
{
    /// <summary>
    /// Represents a native UTF32 codepoint.
    /// </summary>
    public struct CodePoint
    {
        public readonly uint Value;

        public unsafe char ToChar()
        {
            uint value = this.Value;

            char result;

            Encoding.UTF32.GetChars((byte*)&value, 4, &result, 1);

            return result;
        }

        public override string ToString()
        {
            return this.ToChar().ToString();
        }
    }
}
