/* Author:      Shawn
 * Date:        2/24/2015 2:09:20 PM
 * Project:     
 * Description:
 *  
 */

using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeartOfDarkness.Scripting
{
    /// <summary>
    /// Represents a Lua scripting context
    /// </summary>
    public class LuaContext
    {
        /// <summary>
        /// The underlying Lua context to use
        /// </summary>
        Lua m_luaContext;
        
        /// <summary>
        /// Creates a new Lua Context
        /// </summary>
        public LuaContext()
        {
            m_luaContext = new Lua();
            Logger.LogMessage("Starting Lua version {0}", m_luaContext.DoString("return _VERSION"));

            m_luaContext.LoadCLRPackage();
            Logger.LogMessage("Loaded Lua CLR");
        }

        /// <summary>
        /// Registers a global variable with a given name to this context
        /// </summary>
        /// <param name="name">The name of the global variable to register</param>
        public void RegisterVariable(string name)
        {
            m_luaContext.DoString(string.Format("{0}=null", name));
            Logger.LogMessage(LogMessageType.Script, "Registering global Lua variable \"{0}\"", name);
        }

        /// <summary>
        /// Registers a global variable with a given name to this context
        /// </summary>
        /// <typeparam name="T">The C# type to store in the variable</typeparam>
        /// <param name="name">The name of the global variable to register</param>
        /// <param name="initValue">The initial value of the variable</param>
        public void RegisterVariable<T>(string name, T initValue)
        {
            m_luaContext.DoString(string.Format("{0}=null", name));
            m_luaContext[name] = initValue;
            Logger.LogMessage(LogMessageType.Script, "Registering global Lua variable \"{0}\"", name);
        }
        
        /// <summary>
        /// Sets a global variable to the given value
        /// </summary>
        /// <typeparam name="T">The C# type of the value to set</typeparam>
        /// <param name="name">The name of the global variable</param>
        /// <param name="value">The value to set the variable to</param>
        public void SetVariable<T>(string name, T value)
        {
            m_luaContext[name] = value;
        }

        /// <summary>
        /// Gets the global variable with the given name
        /// </summary>
        /// <typeparam name="T">The C# type to return</typeparam>
        /// <param name="name">The name of the variable to get</param>
        /// <returns>The global variable with the given name</returns>
        public T GetVariable<T>(string name)
        {
            return (T)m_luaContext[name];
        }

        /// <summary>
        /// Performs a Lua function
        /// </summary>
        /// <param name="function">The Lua Function to perform</param>
        /// <returns>The results of the script</returns>
        public object[] DoString(string function)
        {
            return m_luaContext.DoString(function);
        }

        /// <summary>
        /// Performs the contents of a Lua script file
        /// </summary>
        /// <param name="path">The path to the Lua script file</param>
        /// <returns>The results of the script</returns>
        public object[] DoFile(string path)
        {
            return m_luaContext.DoFile(path);
        }

        /// <summary>
        /// Creates a Lua Function object from a file
        /// </summary>
        /// <param name="path">The path to the Lua script file</param>
        /// <returns>A LuaFunction created from the given path</returns>
        public LuaFunction LoadFile(string path)
        {
            return m_luaContext.LoadFile(path);
        }

        /// <summary>
        /// Creates a Lua Function object from a string
        /// </summary>
        /// <param name="function">The text containing the Lua function</param>
        /// <param name="commandName">The name for this Lua Command</param>
        /// <returns>A LuaFunction created from the given string</returns>
        public LuaFunction LoadString(string function, string commandName)
        {
            return m_luaContext.LoadString(function, commandName);
        }

        /// <summary>
        /// Registers a Lua function for use by all Lua functions
        /// </summary>
        /// <param name="function">The Lua function to register</param>
        /// <param name="commandName">The name to give to this Lua function</param>
        public void RegisterLuaFunction(string function, string commandName)
        {
            RegisterVariable(commandName);
            SetVariable(commandName, m_luaContext.LoadString(function, commandName));
            Logger.LogMessage(LogMessageType.Script, "Registering global Lua function \"{0}\"", commandName);
        }

        /// <summary>
        /// Registers a Lua function for use by all Lua functions
        /// </summary>
        /// <param name="function">The Lua function to register</param>
        /// <param name="commandName">The name to give to this Lua function</param>
        public void RegisterLuaFunction(LuaFunction function, string commandName)
        {
            RegisterVariable(commandName);
            SetVariable(commandName, function);
            Logger.LogMessage(LogMessageType.Script, "Registering global Lua function \"{0}\"", commandName);
        }
    }
}
