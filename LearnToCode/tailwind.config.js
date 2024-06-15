/** @type {import('tailwindcss').Config} */
module.exports = {
    safelist: [
        '!duration-[0ms]',
        '!delay-[0ms]',
        'html.js :where([class*="taos:"]:not(.taos-init))'
    ],
    content: {
        relative: true,
        transform: (content) => content.replace(/taos:/g, ''),
        files: ["./**/*.{html,razor,razor.cs}"],
    },
    theme: {
        extend: {},
    },
    plugins: [
        require('@tailwindcss/forms'),
        require('taos/plugin'),
        require('tailwind-scrollbar'),
    ],
}