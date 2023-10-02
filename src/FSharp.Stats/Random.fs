﻿namespace FSharp.Stats

/// Uniform random number generators
module Random = 
    open System.Threading
    open System

    /// Interface that every uniform random number generator must implement.
    type IRandom = interface
        abstract NextInt : unit -> int
        abstract NextInt : int -> int
        abstract NextFloat : unit -> float
    end

    ///// A standard implementation of a uniform random source using System.Random()
    type RandThreadSafe =
        val mutable rnd : ThreadLocal<Random>
        /// Constructs the default random number generator using a time dependent default seed value.
        new() = { rnd = new ThreadLocal<Random>(fun () -> new Random()) }
        /// If n is negative, the random number generator seed is based on system time, if it is zero or positive it will
        /// use n as the seed.
        new(n) as this = { rnd = new ThreadLocal<Random>(fun () -> new Random()) }
                         then
                            if n >= 0 then this.rnd <- new ThreadLocal<Random>(fun () -> new Random(n))
        interface IRandom with
            member x.NextInt() = x.rnd.Value.Next()
            ///maxValue is not part of the possible sampling range (minVal &lt;= x &lt; maxVal)
            member x.NextInt maxValue = x.rnd.Value.Next(maxValue)
            /// Returns a random floating-point number that is greater or equal to 0.0, and less then 1.0
            member x.NextFloat() =x.rnd.Value.NextDouble()
        end

    type RandBasic =
        val mutable rnd : Random
        /// Constructs the default random number generator using a time dependent default seed value.
        new() = { rnd = new Random() }
        /// If n is negative, the random number generator seed is based on system time, if it is zero or positive it will
        /// use n as the seed.
        new(n) as this = { rnd = new Random() }
                         then
                            if n >= 0 then this.rnd <- new Random(n)
        interface IRandom with
            ///maxValue is not part of the possible sampling range (minVal &lt;= x &lt; maxVal)
            member x.NextInt() = x.rnd.Next()
            member x.NextInt maxValue = x.rnd.Next(maxValue)
            member x.NextFloat() =x.rnd.NextDouble()
        end

    /// <summary>The uniform random source used for sampling functions.</summary>
    /// <remarks></remarks>
    /// <param name="rndgen"></param>
    /// <returns></returns>
    /// <example>
    /// <code>
    /// </code>
    /// </example>
    let mutable rndgen = new RandThreadSafe() :> IRandom
    /// <summary>Sets the random number generator used for sampling.</summary>
    /// <remarks></remarks>
    /// <param name="rg"></param>
    /// <returns></returns>
    /// <example>
    /// <code>
    /// </code>
    /// </example>
    let SetSampleGenerator rg = rndgen <- rg
    /// Returns the random number generator used for sampling.
    let GetSampleGenerator () = rndgen

    /// random number sampling method for pairs of independent, standard, normally distributed random numbers
    let boxMullerTransform() =
        let (u1,u2) = rndgen.NextFloat(),rndgen.NextFloat()
        let z0 = sqrt(-2. * log u1) * cos (2. * pi * u2)
        let z1 = sqrt(-2. * log u1) * sin (2. * pi * u2)
        z0,z1
        