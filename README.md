![](docs/img/logo_title.svg)

[![DOI](https://zenodo.org/badge/DOI/10.5281/zenodo.6337056.svg)](https://doi.org/10.5281/zenodo.6337056)
[![Discord](https://img.shields.io/discord/836161044501889064?color=purple&label=Join%20our%20Discord%21&logo=discord&logoColor=white)](https://discord.gg/y95XRJg23e)
[![Generic badge](https://img.shields.io/badge/Made%20with-FSharp-rgb(1,143,204).svg)](https://shields.io/)
![GitHub contributors](https://img.shields.io/github/contributors/CSBiology/FSharp.Stats)
[![Build status](https://ci.appveyor.com/api/projects/status/gjsjlqmrljtty780/branch/developer?svg=true)](https://ci.appveyor.com/project/kMutagene/fsharp-stats/branch/developer)
[![codecov](https://codecov.io/gh/fslaborg/FSharp.Stats/branch/developer/graph/badge.svg?token=LRBZPV6MH8)](https://codecov.io/gh/fslaborg/FSharp.Stats)

FSharp.Stats is a multipurpose project for statistical testing, linear algebra, machine learning, fitting and signal processing.

<br>

##### Amongst others, following functionalities are covered:

 
|Descriptive statistics|Fitting|Interpolation|Signal processing|
|:---|:---|:---|:---|
| <ul><li>[Measures of central tendency](https://fslab.org/FSharp.Stats/BasicStats.html)</li><li>[Measures of dispersion](https://fslab.org/FSharp.Stats/BasicStats.html)</li><li>[Correlation](https://fslab.org/FSharp.Stats/Correlation.html)</li><li>Quantile/Rank</li><li>[Distribution](https://fslab.org/FSharp.Stats/Distributions.html)</li></ul> | <ul><li>[Linear regression](https://fslab.org/FSharp.Stats/Fitting.html#Linear-Regression)</li><li>[Nonlinear regression](https://fslab.org/FSharp.Stats/Fitting.html#Nonlinear-Regression)</li><li>[Spline regression](https://fslab.org/FSharp.Stats/Fitting.html#Nonlinear-Regression)</li><li>[Goodness of fit](https://fslab.org/FSharp.Stats/GoodnessOfFit.html)</li></ul><br>|<ul><li>[Polynomial interpolation](https://fslab.org/FSharp.Stats/Interpolation.html#Polynomial-Interpolation)</li><li>[Spline interpolation](https://fslab.org/FSharp.Stats/Interpolation.html#Cubic-interpolating-Spline)</li></ul><br><br><br>|<ul><li>[Continuous wavelet transform](https://fslab.org/FSharp.Stats/Signal.html#Continuous-Wavelet)</li><li>[Smoothing filters](https://fslab.org/FSharp.Stats/Signal.html)</li><li>Peak detection</li></ul><br><br>|
|**Linear algebra**				|**Machine Learning**|**Optimization**			|**Testing**					|
|<ul><li>Singular value decomposition</li></ul><br><br><br><br><br>|<ul><li>PCA</li><li>[Clustering](https://fslab.org/FSharp.Stats/Clustering.html)</li><li>Surprisal analysis</li></ul><br><br><br>|<ul><li>Brent minimization</li><li>Bisection</li></ul><br><br><br><br>|<ul><li>[t test](https://fslab.org/FSharp.Stats/Testing.html#T-Test), [H test](https://fslab.org/FSharp.Stats/Testing.html#H-Test), etc.</li><li>[ANOVA](https://fslab.org/FSharp.Stats/Testing.html#Anova)</li><li>[Post hoc tests](https://fslab.org/FSharp.Stats/Testing.html#PostHoc)</li><li>[Q values](https://fslab.org/FSharp.Stats/Testing.html#Q-Value)</li><li>SAM</li><li>RMT</li></ul>|



## Documentation

Indepth explanations, tutorials and general information about the project can be found [here](https://fslab.org/FSharp.Stats) or at [fslab](https://fslab.org/).
The documentation and tutorials for this library are automatically generated (using the F# Formatting) from *.fsx and *.md files in the docs folder. If you find a typo, please submit a pull request!


## Contributing

Please refer to the [Contribution guidelines](.github/CONTRIBUTING.md).

## Development

to build the project, run either `build.cmd` or `build.sh` depending on your OS.

build targets are defined in the modules of /build/build.fsproj. 

Some interesting targets may be:

`./build.cmd runtests` will build the project and run tests
`./build.cmd watchdocs` will build the project, run tests, and build and host a local version of the documentation.
`./build.cmd release` will start the full release pipeline.


## Library license

The library is available under Apache 2.0. For more information see the License file in the GitHub repository.
