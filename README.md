NRobots.txt [![Build Status](https://ci.appveyor.com/api/projects/status/u7j3d0fn580e8o8y?svg=true)](https://ci.appveyor.com/project/sjdirect/nrobots)
====

A robots.txt parser written in c#.

This is an unofficial & unsupported fork of NRobots.txt on codeplex with extended functionality. For full documentation see https://nrobots.codeplex.com/.

## Project Description:
The Robots Exclusion Protocol or robots.txt protocol, is a convention to prevent cooperating web spiders and other web robots from accessing all or part of a website which is otherwise publicly viewable. 
This project provides an easy-to-use class, implemented in C#, to work with robots.txt files.

## Features:
* Loading Robots.txt files by providing Url or file content
* Easy-to-use and simple usage
* Fluent interface
* Supports multiple User-Agents
* Supports different types of entries:
* Disallow entries
* Allow entries
* Sitemap entries
* Crawl-delay entries
* Supports comments
* Supports wild cards (both * and $)
