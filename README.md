<a name="readme-top"></a>

<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/DimitarM45/Fabula">
    <img src="Fabula.Web/wwwroot/images/FabulaLogo.png" alt="Logo" width="1000" height="500">
  </a>

<h1 align="center">Fabula</h1>

  <p align="center">
    Welcome to Fabula, a web application built for the purposes of sharing, and interacting with user-written literary pieces.
    <br />
    <a href="https://github.com/DimitarM45/Fabula"><strong>Explore the docs »</strong></a>
  </p>
</div>

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![License][license-shield]][license-url]

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li><a href="#overview-of-core-concept-and-functionality">Overview of core concept and functionality</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project

Fabula is a web project built entirely with ASP.NET Core, using Microsoft SQL Server (SQL) as its database. It also makes use of Bootstrap and MDBootstrap and some JavaScript libraries that provide additional functionality and aid in making the website more interactive and user-friendly. The platform was built as part of my assignment for [SoftUni's C# Web Advanced Course](https://softuni.bg/modules/108/csharp-web/1404) final exam.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Built With

* [![Bootstrap][Bootstrap.com]][Bootstrap-url]
* [![JQuery][JQuery.com]][JQuery-url]
* [SimpleMDE Markdown Editor](https://simplemde.com/)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Overview of core concept and functionality

Below is a thorough explanation of the main functionality of Fabula that provides clarity and is mostly meant for the examiner(s) should they find it necessary.
The explanations are separated into 7 categories, one for each entity.

The following image is a representation of Fabula's database structure.

![resources](https://docs.google.com/drawings/d/e/2PACX-1vQmPs7h_ddV2VTg5-pLqY93AP6PvxxctLXX66WlQhJZ5dHcB2EO6Y2Fghdv7FtCYaKXqQkw0AacFcbc/pub?w=1784&h=873)

### Composition
The composition entity is the core of Fabula's concept and, therefore, database structure. Compositions or "works" represent the user-written literary pieces. I chose the word "composition" since it best encapsulates all possible literary genres. Compositions are categorized by genre, where each work can have at least one or multiple genres. Each composition also has an author, comments, ratings and it may itself be a part of a list.

### Genre
The genre entity is paramount in categorizing user-created works. Fabula has a relatively big collection of genres which users could navigate with ease and add their desired genres to their works. Genres can also be favorited w  

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- LICENSE -->
## License

Distributed under the GNU General Public License v3.0. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTACT -->
## Contact

Dimitar Minchev - dimitar45minchev@gmail.com

Project Link: [https://github.com/DimitarM45/Fabula](https://github.com/github_username/repo_name)

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/DimitarM45/Fabula.svg?style=for-the-badge
[contributors-url]: https://github.com/DimitarM45/Fabula/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/DimitarM45/Fabula.svg?style=for-the-badge
[forks-url]: https://github.com/DimitarM45/Fabula/network/members
[stars-shield]: https://img.shields.io/github/stars/DimitarM45/Fabula.svg?style=for-the-badge
[stars-url]: https://github.com/DimitarM45/Fabula/stargazers
[issues-shield]: https://img.shields.io/github/issues/DimitarM45/Fabula.svg?style=for-the-badge
[issues-url]: https://github.com/DimitarM45/Fabula/issues
[license-shield]: https://img.shields.io/github/license/DimitarM45/Fabula.svg?style=for-the-badge
[license-url]: https://github.com/DimitarM45/Fabula/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/linkedin_username
[product-screenshot]: images/screenshot.png
[AspNetCore-url]: https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-6.0
[Bootstrap.com]: https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white
[Bootstrap-url]: https://getbootstrap.com
[JQuery.com]: https://img.shields.io/badge/jQuery-0769AD?style=for-the-badge&logo=jquery&logoColor=white
[JQuery-url]: https://jquery.com 

