@ECHO OFF
cd assets/css
curl -X POST -s --data-urlencode input@style.css https://cssminifier.com/raw > style.min.css
curl -X POST -s --data-urlencode input@slk.css https://cssminifier.com/raw > slk.min.css

del init.css
type vendor\bootstrap.min.css >> init.css
type style.min.css >> init.css

del bundle.css
type vendor\vendor.min.css >> bundle.css
type plugins\plugins.min.css >> bundle.css
type style.min.css >> bundle.css
type slk.min.css >> bundle.css
type ..\toasts\jquery.toast.min.css >> bundle.css

cd ..\js
curl -X POST -s --data-urlencode input@main.js https://www.toptal.com/developers/javascript-minifier/raw > main.min.js
del bundle.js
rem type vendor\jquery-3.5.1.min.js >> bundle.js
type vendor\jquery-migrate-3.3.0.min.js >> bundle.js
type vendor\bootstrap.bundle.min.js >> bundle.js
type vendor\modernizr-3.6.0.min.js >> bundle.js
type plugins\jquery-ui.js >> bundle.js
type plugins\jquery-ui-touch-punch.js >> bundle.js
type plugins\slick.js >> bundle.js
type plugins\countdown.js >> bundle.js
type plugins\wow.js >> bundle.js
type plugins\instafeed.js >> bundle.js
type plugins\svg-injector.min.js >> bundle.js
type plugins\jquery.nice-select.min.js >> bundle.js
type plugins\mouse-parallax.js >> bundle.js
type plugins\images-loaded.js >> bundle.js
type plugins\isotope.js >> bundle.js
type plugins\magnific-popup.js >> bundle.js
type plugins\easyzoom.js >> bundle.js
type plugins\scrollup.js >> bundle.js
rem type plugins\ajax-mail.js >> bundle.js
type ..\toasts\jquery.toast.min.js >> bundle.js
type main.min.js >> bundle.js
PAUSE