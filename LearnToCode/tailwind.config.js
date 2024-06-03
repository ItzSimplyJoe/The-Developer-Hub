/** @type {import('tailwindcss').Config} */
module.exports = {
    content: {
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