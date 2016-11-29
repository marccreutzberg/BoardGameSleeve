/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var sass = require('gulp-sass');
var cleanCSS = require('gulp-clean-css');

gulp.task('sass', function () {
    return gulp.src('./Sass/**/*.scss')
      .pipe(sass().on('error', sass.logError))
      .pipe(gulp.dest('../BoardGameSleeveWebsite/css'));
});

gulp.task('sass:build', function () {
    return gulp.src('./Sass/**/*.scss')
      .pipe(sass().on('error', sass.logError))
      .pipe(cleanCSS({
          //removing comments /!* ... */
          keepSpecialComments: 0
      }))
      .pipe(gulp.dest('../BoardGameSleeveWebsite/css'));
});

gulp.task('sass:watch', function () {
    gulp.watch('./Sass/**/*.scss', ['sass']);
});

