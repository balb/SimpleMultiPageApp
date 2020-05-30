module.exports = {
    root: true,
    parser: '@typescript-eslint/parser',
    parserOptions: {
        tsconfigRootDir: __dirname,
        project: ['./tsconfig.json'],
    },
    settings: {
        react: {
            version: "detect"
        }
    },
    plugins: [
        '@typescript-eslint'
    ],
    extends: [
        'eslint:recommended',
        'plugin:@typescript-eslint/recommended',
        'plugin:@typescript-eslint/recommended-requiring-type-checking',
        "plugin:react/recommended",
        "plugin:react-hooks/recommended"
    ],
};